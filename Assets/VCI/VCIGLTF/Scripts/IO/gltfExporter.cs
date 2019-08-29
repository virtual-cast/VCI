using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;

#endif


namespace VCIGLTF
{
    public class gltfExporter : IDisposable
    {
        private const string CONVERT_HUMANOID_KEY = UniGLTFVersion.MENU + "/Export";

#if UNITY_EDITOR
        [MenuItem(CONVERT_HUMANOID_KEY, true, 1)]
        private static bool ExportValidate()
        {
            return Selection.activeObject != null && Selection.activeObject is GameObject;
        }

        [MenuItem(CONVERT_HUMANOID_KEY, false, 1)]
        private static void ExportFromMenu()
        {
            var go = Selection.activeObject as GameObject;
            var path = EditorUtility.SaveFilePanel(
                "Save glb",
                "",
                go.name + ".glb",
                "glb");
            if (string.IsNullOrEmpty(path)) return;

            var gltf = new glTF();
            using (var exporter = new gltfExporter(gltf))
            {
                exporter.Prepare(go);
                exporter.Export();
            }

            var bytes = gltf.ToGlbBytes();
            File.WriteAllBytes(path, bytes);

            if (path.StartsWithUnityAssetPath())
            {
                AssetDatabase.ImportAsset(path.ToUnityRelativePath());
                AssetDatabase.Refresh();
            }
        }
#endif

        private glTF glTF;
        public glTF GLTF => glTF;

        public bool UseSparseAccessorForBlendShape { get; set; }

        public GameObject Copy { get; protected set; }

        public List<Mesh> Meshes { get; private set; }

        public List<Transform> Nodes { get; private set; }

        public List<Material> Materials { get; set; }

        public TextureExportManager TextureManager;

        protected virtual IMaterialExporter CreateMaterialExporter()
        {
            return new MaterialExporter();
        }

        /// <summary>
        /// このエクスポーターがサポートするExtension
        /// </summary>
        protected virtual IEnumerable<string> ExtensionUsed
        {
            get { yield return glTF_KHR_materials_unlit.ExtensionName; }
        }

        public gltfExporter(glTF gltf)
        {
            glTF = gltf;

            glTF.extensionsUsed.AddRange(ExtensionUsed);

            glTF.asset = new glTFAssets
            {
                generator = "UniVCI-" + VCI.VCIVersion.VERSION,
                version = "2.0",
            };
        }

        /// <summary>
        /// Copyを削除する
        /// </summary>
        public void Dispose()
        {
            if (Application.isEditor)
                Object.DestroyImmediate(Copy);
            else
                Object.Destroy(Copy);
        }

        /// <summary>
        /// コピーを作って、Z軸を反転することで左手系を右手系に変換する
        /// </summary>
        /// <param name="go"></param>
        public virtual void Prepare(GameObject go)
        {
            Copy = Object.Instantiate(go);
            Copy.transform.ReverseZRecursive();

            Nodes = Copy.transform.Traverse()
                .Skip(1) // exclude root object for the symmetry with the importer
                .ToList();
            Materials = Nodes.SelectMany(x => x.GetSharedMaterials()).Where(x => x != null).Distinct().ToList();
        }

        /// <summary>
        /// Prepareで作ったCopyから、glTFにデータを変換する
        /// </summary>
        public virtual void Export()
        {
            var bytesBuffer = new ArrayByteBuffer(new byte[50 * 1024 * 1024]);
            var bufferIndex = glTF.AddBuffer(bytesBuffer);

            #region Materials and Textures

            var unityTextures = Materials.SelectMany(x => TextureIO.GetTextures(x)).Where(x => x.Texture != null)
                .Distinct().ToList();

            TextureManager = new TextureExportManager(unityTextures.Select(x => x.Texture));

            var materialExporter = CreateMaterialExporter();
            glTF.materials = Materials.Select(x => materialExporter.ExportMaterial(x, TextureManager)).ToList();

            for (var i = 0; i < unityTextures.Count; ++i)
            {
                var unityTexture = unityTextures[i];
                TextureIO.ExportTexture(glTF, bufferIndex, TextureManager.GetExportTexture(i),
                    unityTexture.TextureType);
            }

            #endregion

            if (Copy != null)
            {
                #region Meshes

                var unityMeshes = Nodes
                    .Select(x => new MeshWithRenderer
                    {
                        Mesh = x.GetSharedMesh(),
                        Rendererer = x.GetComponent<Renderer>(),
                    })
                    .Where(x =>
                    {
                        if (x.Mesh == null) return false;
                        if (x.Rendererer.sharedMaterials == null
                            || x.Rendererer.sharedMaterials.Length == 0)
                            return false;

                        return true;
                    })
                    .ToList();
                MeshExporter.ExportMeshes(glTF, bufferIndex, unityMeshes, Materials, UseSparseAccessorForBlendShape);
                Meshes = unityMeshes.Select(x => x.Mesh).ToList();

                #endregion

                #region Nodes and Skins

                var unitySkins = Nodes
                    .Select(x => x.GetComponent<SkinnedMeshRenderer>()).Where(x =>
                        x != null
                        && x.bones != null
                        && x.bones.Length > 0)
                    .ToList();
                glTF.nodes = Nodes.Select(x =>
                    ExportNode(x, Nodes, unityMeshes.Select(y => y.Rendererer).ToList(), unitySkins)).ToList();
                glTF.scenes = new List<gltfScene>
                {
                    new gltfScene
                    {
                        nodes = Copy.transform.GetChildren().Select(x => Nodes.IndexOf(x)).ToArray(),
                    }
                };

                foreach (var x in unitySkins)
                {
                    var matrices = x.sharedMesh.bindposes.Select(y => y.ReverseZ()).ToArray();
                    var accessor = glTF.ExtendBufferAndGetAccessorIndex(bufferIndex, matrices, glBufferTarget.NONE);

                    var skin = new glTFSkin
                    {
                        inverseBindMatrices = accessor,
                        joints = x.bones.Select(y => Nodes.IndexOf(y)).ToArray(),
                        skeleton = Nodes.IndexOf(x.rootBone),
                    };
                    var skinIndex = glTF.skins.Count;
                    glTF.skins.Add(skin);

                    foreach (var z in Nodes.Where(y => y.Has(x)))
                    {
                        var nodeIndex = Nodes.IndexOf(z);
                        var node = glTF.nodes[nodeIndex];
                        node.skin = skinIndex;
                    }
                }

                #endregion

#if UNITY_EDITOR

                #region Animations

                var clips = new List<AnimationClip>();
                var animator = Copy.GetComponent<Animator>();
                var animation = Copy.GetComponent<Animation>();
                if (animator != null)
                    clips = AnimationExporter.GetAnimationClips(animator);
                else if (animation != null) clips = AnimationExporter.GetAnimationClips(animation);

                if (clips.Any())
                {
                    foreach (var clip in clips)
                    {
                        var animationWithCurve = AnimationExporter.Export(clip, Copy.transform, Nodes);
                        AnimationExporter.WriteAnimationWithSampleCurves(glTF, animationWithCurve, clip.name, bufferIndex);
                    }
                }
                #endregion

#endif
            }
        }

        private static glTFNode ExportNode(Transform x, List<Transform> nodes, List<Renderer> renderers,
            List<SkinnedMeshRenderer> skins)
        {
            var node = new glTFNode
            {
                name = x.name,
                children = x.transform.GetChildren().Select(y => nodes.IndexOf(y)).ToArray(),
                rotation = x.transform.localRotation.ToArray(),
                translation = x.transform.localPosition.ToArray(),
                scale = x.transform.localScale.ToArray(),
            };

            if (x.gameObject.activeInHierarchy)
            {
                var meshRenderer = x.GetComponent<MeshRenderer>();
                if (meshRenderer != null) node.mesh = renderers.IndexOf(meshRenderer);

                var skinnredMeshRenderer = x.GetComponent<SkinnedMeshRenderer>();
                if (skinnredMeshRenderer != null)
                {
                    node.mesh = renderers.IndexOf(skinnredMeshRenderer);
                    node.skin = skins.IndexOf(skinnredMeshRenderer);
                }
            }

            return node;
        }
    }
}