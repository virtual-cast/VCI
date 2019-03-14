using VCIDepthFirstScheduler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VCIGLTF;
using VCIJSON;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VCI
{
    public class VCIImporter : ImporterContext
    {
        public Dictionary<string, string> AudioAssetPathList = new Dictionary<string, string>();

        public VCIObject VCIObject
        {
            get;
            private set;
        }

        public VCIImporter()
        {
            UseUniJSONParser = true;
        }

        public override void Parse(string path, byte[] bytes)
        {
            var ext = Path.GetExtension(path).ToLower();
            switch (ext)
            {
                case ".vci":
                    ParseGlb(bytes);
                    break;

                default:
                    base.Parse(path, bytes);
                    break;
            }
        }

        const string MATERIAL_EXTENSION_NAME = "VCAST_vci_material_unity";

        const string MATERIAL_PATH = "/extensions/"+MATERIAL_EXTENSION_NAME+"/materials";

        public override void ParseJson(string json, IStorage storage)
        {
            base.ParseJson(json, storage);
            var parsed = JsonParser.Parse(Json);
            if (parsed.ContainsKey("extensions")
                && parsed["extensions"].ContainsKey(MATERIAL_EXTENSION_NAME)
                && parsed["extensions"][MATERIAL_EXTENSION_NAME].ContainsKey("materials")
                )
            {
                var nodes = parsed["extensions"][MATERIAL_EXTENSION_NAME]["materials"];
                SetMaterialImporter(new VCI.VCIMaterialImporter(this, VCI.glTF_VCI_Material.Parse(nodes)));
            }
        }

        protected override void OnLoadModel()
        {
            if (GLTF.extensions.VCAST_vci_meta == null)
            {
                base.OnLoadModel();
                return;
            }
        }

        public IEnumerator SetupCorutine()
        {
            VCIObject = Root.AddComponent<VCIObject>();
            yield return ToUnity(meta => VCIObject.Meta = meta);

            // Script
            if (GLTF.extensions.VCAST_vci_embedded_script != null)
            {
                VCIObject.Scripts.AddRange(GLTF.extensions.VCAST_vci_embedded_script.scripts.Select(x =>
                {
                    var source = "";
                    try
                    {
                        var bytes = GLTF.GetViewBytes(x.source);
                        source = Utf8String.Encoding.GetString(bytes.Array, bytes.Offset, bytes.Count);
                    }
                    catch(Exception)
                    {
                        // 握りつぶし
                    }

                    return new VCIObject.Script
                    {
                        name = x.name,
                        mimeType = x.mimeType,
                        targetEngine = x.targetEngine,
                        source = source
                    };
                }));
            }

            // Audio
            if (GLTF.extensions.VCAST_vci_audios != null 
                && GLTF.extensions.VCAST_vci_audios.audios != null)
            {
                var root = this.Root;
                foreach (var audio in GLTF.extensions.VCAST_vci_audios.audios)
                {
#if ((NET_4_6 || NET_STANDARD_2_0) && UNITY_2017_1_OR_NEWER)
                    if (Application.isPlaying)
                    {
                        var bytes = GLTF.GetViewBytes(audio.bufferView);
                        WaveUtil.WaveHeaderData waveHeader;
                        byte[] audioBuffer = new byte[bytes.Count];
                        Buffer.BlockCopy(bytes.Array, bytes.Offset, audioBuffer, 0, audioBuffer.Length);
                        if (WaveUtil.TryReadHeader(audioBuffer, out waveHeader))
                        {
                            AudioClip audioClip = null;
                            yield return AudioClipMaker.Create(
                                audio.name,
                                audioBuffer,
                                44,
                                waveHeader.BitPerSample,
                                waveHeader.DataChunkSize / waveHeader.BlockSize,
                                waveHeader.Channel,
                                waveHeader.SampleRate,
                                false,
                                (clip)=> {audioClip = clip;});
                            if (audioClip != null)
                            {
                                var source = root.AddComponent<AudioSource>();
                                source.clip = audioClip;
                            }
                        }
                    }
                    else
#endif
                    {
#if UNITY_EDITOR
                        var audioClip = AssetDatabase.LoadAssetAtPath(AudioAssetPathList[audio.name], typeof(AudioClip)) as AudioClip;
                        if (audioClip != null)
                        {
                            var source = root.AddComponent<AudioSource>();
                            source.clip = audioClip;
                        }
                        else
                        {
                            Debug.LogWarning("AudioFile NotFound: " + audio.name);
                        }
#endif
                    }
                }
            }

            for (int i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;
                if (node.extensions != null)
                {
                    if (node.extensions.VCAST_vci_item != null)
                    {
                        var item = go.AddComponent<VCISubItem>();
                        item.Grabbable = node.extensions.VCAST_vci_item.grabbable;
                        item.Scalable = node.extensions.VCAST_vci_item.scalable;
                        item.UniformScaling = node.extensions.VCAST_vci_item.uniformScaling;
                        item.GroupId = node.extensions.VCAST_vci_item.groupId;
                    }
                }
            }
        }

        /// <summary>

        /// 

        /// </summary>

        /// <param name="replace">Rootオブジェクトのセットアップを1階層上げるのに使う</param>
        public void SetupPhysics(GameObject rootReplaceTarget=null)
        {
            // Collider and Rigidbody
            for (int i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;

                if (go == Root && rootReplaceTarget != null)
                {
                    go = rootReplaceTarget;
                }               

                if (node.extensions != null)
                {
                    if (node.extensions.VCAST_vci_collider != null
                        && node.extensions.VCAST_vci_collider.colliders != null)
                    {
                        foreach (var vciCollider in node.extensions.VCAST_vci_collider.colliders)
                        {
                            glTF_VCAST_vci_Collider.AddColliderComponent(go, vciCollider);
                        }
                    }

                    if (node.extensions.VCAST_vci_rigidbody != null
                        && node.extensions.VCAST_vci_rigidbody.rigidbodies != null)
                    {
                        foreach (var rigidbody in node.extensions.VCAST_vci_rigidbody.rigidbodies)
                        {
                            glTF_VCAST_vci_Rigidbody.AddRigidbodyComponent(go, rigidbody);
                        }
                    }
                }
            }

            // Joint
            for (int i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;
                if (go == Root && rootReplaceTarget != null)
                {
                    go = rootReplaceTarget;
                }

                if (node.extensions != null)
                {
                    if (node.extensions.VCAST_vci_joints != null
                        && node.extensions.VCAST_vci_joints.joints != null)
                    {
                        foreach (var joint in node.extensions.VCAST_vci_joints.joints)
                        {
                            glTF_VCAST_vci_joint.AddJointComponent(go, joint, Nodes);
                        }
                    }
                }
            }
        }

        public void ExtractAudio(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            if(GLTF.extensions.VCAST_vci_audios == null 
                || GLTF.extensions.VCAST_vci_audios.audios == null 
                || GLTF.extensions.VCAST_vci_audios.audios.Count == 0)
            {
                return;
            }

            var prefabParentDir = prefabPath.Parent;

            // glb buffer
            var folder = prefabPath.GetAssetFolder(".Audios");

            int created = 0;
            for (int i = 0; i < GLTF.extensions.VCAST_vci_audios.audios.Count; ++i)
            {
                folder.EnsureFolder();
                var audio = GLTF.extensions.VCAST_vci_audios.audios[i];
                var bytes = GLTF.GetViewBytes(audio.bufferView);

                Byte[] audioBuffer = new byte[bytes.Count];
                Buffer.BlockCopy(bytes.Array, bytes.Offset, audioBuffer, 0, audioBuffer.Length);

                var path = string.Format("{0}/{1}.wav", folder.Value, audio.name);
                File.WriteAllBytes(path, audioBuffer);
                AudioAssetPathList.Add(audio.name, path);
                created++;
            }

            if (created > 0)
            {
                AssetDatabase.Refresh();
            }
#endif
        }

#region Meta
        [Serializable]
        public struct Meta
        {
            public string title;
            public string author;
            public string contactInformation;
            public string reference;
            public Texture2D thumbnail;
            public string version;
            
            [SerializeField, TextArea(1,16)]
            public string description;

            public string exporterVersion;
            public string specVersion;


#region Model Data License Url
            [Header("Model Data License")]
            public VCIGLTF.glTF_VCAST_vci_meta.LicenseType modelDataLicenseType;
            public string modelDataOtherLicenseUrl;
#endregion

#region Script License Url
            [Header("Script License")]
            public VCIGLTF.glTF_VCAST_vci_meta.LicenseType scriptLicenseType;
            public string scriptOtherLicenseUrl;
#endregion

#region Script Settings
            [Header("Script Settings")]
            public bool scriptWriteProtected;
            public bool scriptEnableDebugging;

            public VCIGLTF.glTF_VCAST_vci_meta.ScriptFormat scriptFormat;
#endregion
        }

        public IEnumerator ToUnity(Action<Meta> callback)
        {
            if (GLTF.extensions.VCAST_vci_meta == null)
            {
                callback(default(Meta));
                yield break;
            }

            var meta = GLTF.extensions.VCAST_vci_meta;
            if (meta == null)
            {
                callback(default(Meta));
                yield break;
            }

            var texture = this.GetTexture(meta.thumbnail);
            if (texture != null)
            {
                yield return texture.ProcessCoroutine(GLTF, Storage);
            }

            callback(new Meta
            {
                exporterVersion = meta.exporterVCIVersion,
                specVersion = meta.specVersion,

                title = meta.title,
                version = meta.version,
                author = meta.author,
                contactInformation = meta.contactInformation,
                reference = meta.reference,
                description = meta.description,
                thumbnail = texture != null ? texture.Texture : null,

                modelDataLicenseType = meta.modelDataLicenseType,
                modelDataOtherLicenseUrl = meta.modelDataOtherLicenseUrl,
                scriptLicenseType = meta.scriptLicenseType,
                scriptOtherLicenseUrl = meta.scriptOtherLicenseUrl,

                scriptWriteProtected = meta.scriptWriteProtected,
                scriptEnableDebugging = meta.scriptEnableDebugging,
                scriptFormat = meta.scriptFormat,
            });
        }
#endregion

        /// <summary>
        /// VCIファイルとして書き出す
        /// </summary>
        /// <param name="path"></param>
        public void Rewrite(string path)
        {
            /* ToDo
            GLTF.extensions.VCAST_vci_audios = null;
            var bytes = GLTF.ToGlbBytes(true);
            File.WriteAllBytes(path, bytes);
            */
        }
    }
}
