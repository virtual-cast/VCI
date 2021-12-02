using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEngine;
using VRM;
using VRMShaders;

namespace VCI
{

    public sealed class VCIExporter : gltfExporter
    {
        internal static readonly GltfExportSettings VciExportSettings = new GltfExportSettings
        {
            InverseAxis = Axes.Z, // VCI の仕様
            DivideVertexBuffer = default,
            UseSparseAccessorForMorphTarget = true, // ファイル容量削減
            ExportOnlyBlendShapePosition = default,
            ExportTangents = false, // 要らない
            UseEmissiveMultiplier = true, // VCI は同等のものを自前で定義しているが、重複しても問題ない
        };

        private readonly IVciColliderLayerProvider _colliderLayerProvider;

        private List<Transform> _originalNodes = new List<Transform>();

        // NOTE: GltfExportSettings には VCI の仕様が含まれるため、強制的に上書きする.
        public VCIExporter(ExportingGltfData gltfData, IVciColliderLayerProvider colliderLayerProvider = null) : base(gltfData, VciExportSettings)
        {
            _colliderLayerProvider = colliderLayerProvider ?? new VciDefaultLayerSettings();
        }

        protected override IMaterialExporter CreateMaterialExporter()
        {
            return new VRMMaterialExporter();
        }

        public override void Prepare(GameObject go)
        {
            base.Prepare(go);

            // base.Export() と同じ方法で Nodes を構築し、インデックスで対応して参照できるようにする。
            _originalNodes = go.transform.Traverse().Skip(go.transform.childCount == 0 ? 0 : 1).ToList();
        }

        public override void ExportExtensions(ITextureSerializer textureSerializer)
        {
            base.ExportExtensions(textureSerializer);

            var exporter = this;

            var usedExtensions = new HashSet<string>();

            // VCIのmaterial拡張
            {
                var VCAST_vci_material_unity = new glTF_VCAST_vci_material_unity();
                VCAST_vci_material_unity.materials = new List<VciMaterialJsonObject>();
                foreach (var material in exporter.Materials)
                {
                    VCAST_vci_material_unity.materials.Add(VCIMaterialExporter.CreateFromMaterial(material, TextureExporter));
                }

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_material_unity_Serializer.Serialize(f, VCAST_vci_material_unity);
                AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_material_unity.ExtensionName, f.GetStore().Bytes);
            }


            if (Copy == null) return;

            // vci interaction
            var vciObject = Copy.GetComponent<VCIObject>();
            if (vciObject != null)
            {
                // Embedded Script
                var scriptExtension = ScriptExporter.ExportScript(_data, vciObject);
                if (scriptExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_embedded_script_Serializer.Serialize(f, scriptExtension);
                    AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_embedded_script.ExtensionName, f.GetStore().Bytes);
                }

                // SpringBone
                // NOTE: このスコープに記述があるのは変っぽい。もうひとつ外側では？
                var springBonesExtension = SpringBoneExporter.ExportSpringBones(Copy, Nodes);
                if (springBonesExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_spring_bone_Serializer.Serialize(f, springBonesExtension);
                    AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_spring_bone.ExtensionName, f.GetStore().Bytes);
                }

                // meta
                var metaExtension = MetaExporter.ExportMeta(vciObject, TextureExporter);
                if (metaExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_meta_Serializer.Serialize(f, metaExtension);
                    AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_meta.ExtensionName, f.GetStore().Bytes);
                }
            }

            // Audio
            var audiosExtension = AudioExporter.ExportAudioSourcesOnRoot(_data, Copy);
            if (audiosExtension != null)
            {
                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_audios_Serializer.Serialize(f, audiosExtension);
                AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_audios.ExtensionName, f.GetStore().Bytes);
            }

            // 出力キャッシュ
            var colliderMeshExporter = new PhysicsColliderMeshExporter(_data, VciExportSettings.InverseAxis.Create());

            // ノード (Transform) ごとに定義される拡張
            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = _gltf.nodes[i];

                // Colliders
                var collidersExtension = PhysicsColliderExporter.ExportColliders(node, _data, _colliderLayerProvider, colliderMeshExporter);
                if (collidersExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_colliders_Serializer.Serialize(f, collidersExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_colliders.ExtensionName, f.GetStore().Bytes);
                }

                // Rigidbodies
                var rigidbodiesExtension = PhysicsRigidbodyExporter.ExportRigidbodies(node);
                if (rigidbodiesExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_rigidbody_Serializer.Serialize(f, rigidbodiesExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_rigidbody.ExtensionName, f.GetStore().Bytes);
                }

                // Joints
                var jointsExtension = PhysicsJointExporter.ExportJoints(node, exporter.Nodes);
                if (jointsExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_joints_Serializer.Serialize(f, jointsExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_joints.ExtensionName, f.GetStore().Bytes);
                }

                // SubItem
                var subItemExtension = SubItemExporter.ExportSubItem(node);
                if (subItemExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_item_Serializer.Serialize(f, subItemExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_item.ExtensionName, f.GetStore().Bytes);
                }

                // Attachable
                var attachableExtension = AttachableExporter.ExportAttachable(node);
                if (attachableExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_attachable_Serializer.Serialize(f, attachableExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_attachable.ExtensionName, f.GetStore().Bytes);
                }

                // TextMeshPro RectTransform
                var tmpRectTransformExtension = TextMeshProExporter.ExportTextMeshProRectTransform(node);
                if (tmpRectTransformExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_rectTransform_Serializer.Serialize(f, tmpRectTransformExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_rectTransform.ExtensionName, f.GetStore().Bytes);
                }

                // TextMeshPro Text
                var tmpTextExtension = TextMeshProExporter.ExportTextMeshProText(node);
                if (tmpTextExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_text_Serializer.Serialize(f, tmpTextExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_text.ExtensionName, f.GetStore().Bytes);
                }

                // PlayerSpawnPoint
                var pspExtension = PlayerSpawnPointExporter.ExportPlayerSpawnPoint(node);
                if (pspExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_player_spawn_point_Serializer.Serialize(f, pspExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_player_spawn_point.ExtensionName, f.GetStore().Bytes);
                }

                // PlayerSpawnPoint Restriction
                var pspRestrictionExtension = PlayerSpawnPointExporter.ExportPlayerSpawnPointRestriction(node);
                if (pspRestrictionExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_player_spawn_point_restriction_Serializer.Serialize(f, pspRestrictionExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_player_spawn_point_restriction.ExtensionName, f.GetStore().Bytes);
                }

                var audioSourcesExtension = AudioExporter.ExportAudioSourcesOnNode(node, audiosExtension);
                if (audioSourcesExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_audio_sources_Serializer.Serialize(f, audioSourcesExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_audio_sources.ExtensionName, f.GetStore().Bytes);
                }
            }

            // Animation
            var animationExtensions = AnimationNodeExporter.ExportAnimations(_data, Copy, Nodes);
            foreach (var (gltfNode, animationExtension) in animationExtensions)
            {
                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_animation_Serializer.Serialize(f, animationExtension);
                AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_animation.ExtensionName, f.GetStore().Bytes);
            }

            // Effekseer
            var effekseerExporting = EffekseerExporter.ExportEffekseer(_data, Nodes, textureSerializer);
            if (effekseerExporting.HasValue)
            {
                var (effekseerExtension, effekseerEmittersExtensions) = effekseerExporting.Value;

                // Effekseer Emitters
                foreach (var (gltfNode, effekseerEmittersExtension) in effekseerEmittersExtensions)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_Effekseer_emitters_Serializer.Serialize(f, effekseerEmittersExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_Effekseer_emitters.ExtensionName, f.GetStore().Bytes);
                }

                // Effekseer Root
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_Effekseer_Serializer.Serialize(f, effekseerExtension);
                    AddExtensionValue(_gltf, usedExtensions, glTF_Effekseer.ExtensionName, f.GetStore().Bytes);
                }
            }

            // LocationBounds
            var locationBoundsExtension = LocationBoundsExporter.ExportLocationBounds(Copy);
            if (locationBoundsExtension != null)
            {
                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_location_bounds_Serializer.Serialize(f, locationBoundsExtension);
                AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_location_bounds.ExtensionName, f.GetStore().Bytes);
            }

            // Scene Lighting
            if (VciSymbols.IsDevelopmentEnabled)
            {
                var lightmapTextureExporter = new LightmapTextureExporter(TextureExporter);
                var cubemapTextureExporter = new CubemapTextureExporter(TextureExporter);

                // Lightmap Nodes
                var lightmapExtensions = LightmapExporter.ExportLightmaps(
                    _gltf,
                    Nodes,
                    _originalNodes,
                    lightmapTextureExporter);
                foreach (var (gltfNode, lightmapExtension) in lightmapExtensions)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_lightmap_Serializer.Serialize(f, lightmapExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_lightmap.ExtensionName, f.GetStore().Bytes);
                }

                // Scene Lightmap & Lights
                var locationLightingExtension = LightmapExporter.ExportLocationLighting(
                    lightmapExtensions.Count != 0,
                    lightmapTextureExporter,
                    cubemapTextureExporter);
                if (locationLightingExtension != null)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_location_lighting_Serializer.Serialize(f, locationLightingExtension);
                    AddExtensionValue(_gltf, usedExtensions, glTF_VCAST_vci_location_lighting.ExtensionName, f.GetStore().Bytes);
                }

                // Reflection Probe Nodes
                var reflectionProbeExtensions = LightmapExporter.ExportReflectionProbes(
                    _gltf,
                    Nodes,
                    _originalNodes,
                    cubemapTextureExporter);
                foreach (var (gltfNode, reflectionProbeExtension) in reflectionProbeExtensions)
                {
                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_reflectionProbe_Serializer.Serialize(f, reflectionProbeExtension);
                    AddExtensionValue(gltfNode, usedExtensions, glTF_VCAST_vci_reflectionProbe.ExtensionName, f.GetStore().Bytes);
                }
            }

            foreach (var extension in usedExtensions)
            {
                _gltf.extensionsUsed.Add(extension);
            }
        }

        private static void AddExtensionValue(glTF gltf, HashSet<string> usedExtensions, string key, ArraySegment<byte> bytes)
        {
            glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(key, bytes);
            usedExtensions.Add(key);
        }

        private static void AddExtensionValue(glTFNode glTfNode, HashSet<string> usedExtensions, string key,
            ArraySegment<byte> bytes)
        {
            glTFExtensionExport.GetOrCreate(ref glTfNode.extensions).Add(key, bytes);
            usedExtensions.Add(key);
        }
    }
}