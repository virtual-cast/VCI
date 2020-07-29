using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using VCIGLTF;
using VCIJSON;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace VCI
{
    public class VCIImporter : ImporterContext
    {
        public Dictionary<string, string> AudioAssetPathList = new Dictionary<string, string>();

        public List<Effekseer.EffekseerEmitter> EffekseerEmitterComponents = new List<Effekseer.EffekseerEmitter>();
        public List<Effekseer.EffekseerEffectAsset> EffekseerEffectAssets = new List<Effekseer.EffekseerEffectAsset>();

        public VCIObject VCIObject { get; private set; }

        public IFontProvider FontProvider { get; set; }

        public VCIImporter()
        {
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

        private const string MATERIAL_EXTENSION_NAME = "VCAST_vci_material_unity";

        private const string MATERIAL_PATH = "/extensions/" + MATERIAL_EXTENSION_NAME + "/materials";

        private string GetVersionValue(string exportedVciVersion)
        {
            if(string.IsNullOrEmpty(exportedVciVersion))
            {
                throw new Exception("exportedVciVersion is empty.");
            }

            System.Text.RegularExpressions.Regex r =
                new System.Text.RegularExpressions.Regex(
                    @"UniVCI-(?<version>[+-]?[0-9]+[.]?[0-9]([eE][+-])?[0-9])",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    | System.Text.RegularExpressions.RegexOptions.Singleline);
            var match = r.Match(exportedVciVersion);
            var version = match.Groups["version"].Value;
            return version;
        }

        private bool ImportableVersionCheck(string exportedVciVersion)
        {
            var version = GetVersionValue(exportedVciVersion);
            if(string.IsNullOrEmpty(version))
            {
                return false;
            }

            int itemExportedVersion = (int)(float.Parse(version) * 100);
            int uniVciVersion = (int)(float.Parse(VCIVersion.VERSION) * 100);
            if( itemExportedVersion > uniVciVersion)
            {
                return false;
            }

            return true;
        }

        public override void ParseJson(string json, IStorage storage)
        {
            base.ParseJson(json, storage);
            var parsed = JsonParser.Parse(Json);

            if(parsed.ContainsKey("extensions")
                && parsed["extensions"].ContainsKey("VCAST_vci_meta")
            )
            {
                var meta = parsed["extensions"]["VCAST_vci_meta"];
                var version = meta["exporterVCIVersion"];

                if(!ImportableVersionCheck(version.Value.ToString()))
                {
                    throw new Exception("The current UniVCI cannot read this VCI version. " + version.Value);
                }
            }

            if (parsed.ContainsKey("extensions")
                && parsed["extensions"].ContainsKey(MATERIAL_EXTENSION_NAME)
                && parsed["extensions"][MATERIAL_EXTENSION_NAME].ContainsKey("materials")
            )
            {
                // UniVCI v0.27以下のバージョンの場合は、baseColorをsrgbからlinearに変換した後にCreateMaterialを実行する
                bool srgbToLinear = false;
                if(parsed.ContainsKey("extensions")
                    && parsed["extensions"].ContainsKey("VCAST_vci_meta")
                )
                {
                    var meta = parsed["extensions"]["VCAST_vci_meta"];
                    var version = meta["exporterVCIVersion"];

                    int exportedVciVersion = (int)(float.Parse(GetVersionValue(version.Value.ToString())) * 100);
                    if(exportedVciVersion <= 27)
                    {
                        srgbToLinear = true;
                    }
                }

                var nodes = parsed["extensions"][MATERIAL_EXTENSION_NAME]["materials"];
                SetMaterialImporter(new VCIMaterialImporter(this, glTF_VCI_Material.Parse(nodes), srgbToLinear));
                SetAnimationImporter(new EachAnimationImporter());
            }
        }

        protected override IEnumerator OnLoadModel()
        {
            if (GLTF.extensions.VCAST_vci_meta == null)
            {
                base.OnLoadModel();
            }

            yield break;
        }

        public IEnumerator SetupCoroutine()
        {
            VCIObject = Root.AddComponent<VCIObject>();
            yield return ToUnity(meta => VCIObject.Meta = meta);

            // Script
            if (GLTF.extensions.VCAST_vci_embedded_script != null)
                VCIObject.Scripts.AddRange(GLTF.extensions.VCAST_vci_embedded_script.scripts.Select(x =>
                {
                    var source = "";
                    try
                    {
                        var bytes = GLTF.GetViewBytes(x.source);
                        source = Utf8String.Encoding.GetString(bytes.Array, bytes.Offset, bytes.Count);
                    }
                    catch (Exception)
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

            // Audio
            if (GLTF.extensions.VCAST_vci_audios != null
                && GLTF.extensions.VCAST_vci_audios.audios != null)
            {
                var root = Root;
                foreach (var audio in GLTF.extensions.VCAST_vci_audios.audios)
                {
#if ((NET_4_6 || NET_STANDARD_2_0) && UNITY_2017_1_OR_NEWER)
                    if (Application.isPlaying)
                    {
                        var bytes = GLTF.GetViewBytes(audio.bufferView);
                        yield return AudioImporter.Import(audio, bytes, root);
                    }
                    else
#endif
                    {
#if UNITY_EDITOR
                        var audioClip =
                            AssetDatabase.LoadAssetAtPath(AudioAssetPathList[audio.name], typeof(AudioClip)) as
                                AudioClip;
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

            // Animation
            var rootAnimation = Root.GetComponent<Animation>();
            if (rootAnimation != null)
            {
                rootAnimation.playAutomatically = false;
            }

            // SubItem
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                if (node.extensions != null)
                {
                    if (node.extensions.VCAST_vci_item != null)
                    {
                        var go = Nodes[i].gameObject;
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
        public void SetupPhysics(GameObject rootReplaceTarget = null)
        {
            // Collider and Rigidbody
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;

                if (go == Root && rootReplaceTarget != null) go = rootReplaceTarget;

                if (node.extensions != null)
                {
                    if (node.extensions.VCAST_vci_collider?.colliders != null)
                        foreach (var vciCollider in node.extensions.VCAST_vci_collider.colliders)
                            glTF_VCAST_vci_Collider.AddColliderComponent(go, vciCollider);

                    if (node.extensions.VCAST_vci_rigidbody?.rigidbodies != null)
                        foreach (var rigidbody in node.extensions.VCAST_vci_rigidbody.rigidbodies)
                            glTF_VCAST_vci_Rigidbody.AddRigidbodyComponent(go, rigidbody);
                }
            }

            // Joint
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                var go = Nodes[i].gameObject;
                if (go == Root && rootReplaceTarget != null) go = rootReplaceTarget;

                if (node.extensions != null)
                    if (node.extensions.VCAST_vci_joints != null
                        && node.extensions.VCAST_vci_joints.joints != null)
                        foreach (var joint in node.extensions.VCAST_vci_joints.joints)
                            glTF_VCAST_vci_joint.AddJointComponent(go, joint, Nodes);
            }
        }

        // Attachable
        public int SetupAttachable()
        {
            var attachableCount = 0;
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];

                if (node.extensions != null && node.extensions.VCAST_vci_attachable != null)
                {
                    var go = Nodes[i].gameObject;
                    var attachable = go.AddComponent<VCIAttachable>();
                    attachable.AttachableHumanBodyBones = node.extensions.VCAST_vci_attachable.attachableHumanBodyBones.Select(x =>
                    {
                        foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
                        {
                            if (x == bone.ToString())
                            {
                                return bone;
                            }
                        }

                        throw new Exception("unknown AttachableHumanBodyBones: " + x);
                    }).ToArray();

                    attachable.AttachableDistance = node.extensions.VCAST_vci_attachable.attachableDistance;
                    attachable.Scalable = node.extensions.VCAST_vci_attachable.scalable;
                    attachable.Offset = node.extensions.VCAST_vci_attachable.offset;
                    attachableCount++;
                }
            }
            return attachableCount;
        }

        public virtual IEnumerator SetupEffekseer()
        {
            // Effekseer
            if (GLTF.extensions != null && GLTF.extensions.Effekseer != null)
            {
                for (var i = 0; i < GLTF.nodes.Count; i++)
                {
                    var node = GLTF.nodes[i];
                    var go = Nodes[i].gameObject;
                    if (node.extensions != null &&
                        node.extensions.Effekseer_emitters != null &&
                        node.extensions.Effekseer_emitters.emitters != null)
                    {
                        foreach (var emitter in node.extensions.Effekseer_emitters.emitters)
                        {
                            var effectIndex = emitter.effectIndex;

                            if (Application.isPlaying)
                            {
                                var effect = GLTF.extensions.Effekseer.effects[effectIndex];
                                var bodySegment = GLTF.GetViewBytes(effect.body.bufferView);
                                byte[] body = new byte[bodySegment.Count];
                                Buffer.BlockCopy(bodySegment.Array, bodySegment.Offset, body, 0, body.Count());

                                var resourcePath = new Effekseer.EffekseerResourcePath();
                                if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                                {
                                    continue;
                                }

                                yield return null;

                                // Images
                                var effekseerTextures = new List<Effekseer.Internal.EffekseerTextureResource>();
                                if (effect.images != null && effect.images.Any())
                                {
                                    for (int t = 0; t < effect.images.Count; t++)
                                    {
                                        var image = effect.images[t];
                                        var path = resourcePath.TexturePathList[t];
                                        var buffer = GLTF.GetViewBytes(image.bufferView);

                                        if (image.mimeType == glTF_Effekseer_image.MimeTypeString.Png)
                                        {
                                            Texture2D texture = new Texture2D(2, 2);
                                            byte[] copyBuffer = new byte[buffer.Count];
                                            Buffer.BlockCopy(buffer.Array, buffer.Offset, copyBuffer, 0, copyBuffer.Count());
                                            texture.LoadImage(copyBuffer);
                                            effekseerTextures.Add(new Effekseer.Internal.EffekseerTextureResource()
                                            {
                                                path = path,
                                                texture = texture
                                            });
                                        }
                                        else
                                        {
                                            Debug.LogError(string.Format("image format {0} is not suppported.", image.mimeType));
                                        }

                                        yield return null;
                                    }
                                }

                                // Models
                                var effekseerModels = new List<Effekseer.Internal.EffekseerModelResource>();
                                if (effect.models != null && effect.models.Any())
                                {
                                    for (int t = 0; t < effect.models.Count; t++)
                                    {
                                        var model = effect.models[t];
                                        var path = resourcePath.ModelPathList[t];
                                        path = Path.ChangeExtension(path, "asset");
                                        var modelSegment = GLTF.GetViewBytes(model.bufferView);
                                        byte[] modelBuffer = new byte[modelSegment.Count];
                                        Buffer.BlockCopy(modelSegment.Array, modelSegment.Offset, modelBuffer, 0, modelBuffer.Count());

                                        effekseerModels.Add(new Effekseer.Internal.EffekseerModelResource()
                                        {
                                            path = path,
                                            asset = new Effekseer.EffekseerModelAsset() { bytes = modelBuffer }
                                        });

                                        yield return null;
                                    }
                                }

                                Effekseer.EffekseerEffectAsset effectAsset = ScriptableObject.CreateInstance<Effekseer.EffekseerEffectAsset>();
                                effectAsset.name = effect.effectName;
                                effectAsset.efkBytes = body;
                                effectAsset.Scale = effect.scale;
                                effectAsset.textureResources = effekseerTextures.ToArray();
                                effectAsset.modelResources = effekseerModels.ToArray();
                                effectAsset.soundResources = new Effekseer.Internal.EffekseerSoundResource[0];

                                yield return null;

                                var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                                emitterComponent.effectAsset = effectAsset;
                                emitterComponent.playOnStart = emitter.isPlayOnStart;
                                emitterComponent.isLooping = emitter.isLoop;

                                emitterComponent.effectAsset.LoadEffect();
                                EffekseerEmitterComponents.Add(emitterComponent);
                            }
                            else
                            {
#if UNITY_EDITOR
                                var emitterComponent = go.AddComponent<Effekseer.EffekseerEmitter>();
                                emitterComponent.effectAsset = EffekseerEffectAssets[effectIndex];
                                emitterComponent.playOnStart = emitter.isPlayOnStart;
                                emitterComponent.isLooping = emitter.isLoop;
                                EffekseerEmitterComponents.Add(emitterComponent);
#endif
                            }
                        }
                    }
                }
            }

            yield break;
        }

        public void SetupText()
        {
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];

                if(node.extensions != null && node.extensions.VCAST_vci_text != null)
                {
                    var go = Nodes[i].gameObject;
                    var rt = go.AddComponent<RectTransform>();
                    // RectTransformのAddComponentで元のtransformが置き換わるのでNodesも更新する。
                    Nodes[i] = rt.transform;

                    var tmp = go.AddComponent<TextMeshPro>();
                    var vci_text = node.extensions.VCAST_vci_text.text;

                    // Get font
                    if (FontProvider != null)
                    {
                        var font = FontProvider.GetDefaultFont();
                        if (font != null) tmp.font = font;
                    }

                    // Set TMPText
                    tmp.text = vci_text.text;
                    tmp.richText = vci_text.richText;
                    tmp.fontSize = vci_text.fontSize;
                    tmp.autoSizeTextContainer = vci_text.autoSize;
                    tmp.fontStyle = (FontStyles)vci_text.fontStyle;
                    tmp.color = new Color(vci_text.color[0], vci_text.color[1], vci_text.color[2], vci_text.color[3]);
                    tmp.enableVertexGradient = vci_text.enableVertexGradient;
                    tmp.colorGradient = new VertexGradient(
                            new Color(vci_text.topLeftColor[0], vci_text.topLeftColor[1], vci_text.topLeftColor[2], vci_text.topLeftColor[3]),
                            new Color(vci_text.topRightColor[0], vci_text.topRightColor[1], vci_text.topRightColor[2], vci_text.topRightColor[3]),
                            new Color(vci_text.bottomLeftColor[0], vci_text.bottomLeftColor[1], vci_text.bottomLeftColor[2], vci_text.bottomLeftColor[3]),
                            new Color(vci_text.bottomRightColor[0], vci_text.bottomRightColor[1], vci_text.bottomRightColor[2], vci_text.bottomRightColor[3])
                        );
                    tmp.characterSpacing = vci_text.characterSpacing;
                    tmp.wordSpacing = vci_text.wordSpacing;
                    tmp.lineSpacing = vci_text.lineSpacing;
                    tmp.paragraphSpacing = vci_text.paragraphSpacing;
                    tmp.alignment = (TextAlignmentOptions)vci_text.alignment;
                    tmp.enableWordWrapping = vci_text.enableWordWrapping;
                    // overflowModeのインポート時にエラーになる可能性があるので無効にする。
                    //tmp.overflowMode = (TextOverflowModes) vci_text.overflowMode;
                    tmp.enableKerning = vci_text.enableKerning;
                    tmp.extraPadding = vci_text.extraPadding;
                    tmp.margin = new Vector4(vci_text.margin[0], vci_text.margin[1], vci_text.margin[2], vci_text.margin[3]);

                    // Set RectTransform
                    var vci_rt = node.extensions.VCAST_vci_rectTransform.rectTransform;
                    rt.anchorMin = new Vector2(vci_rt.anchorMin[0], vci_rt.anchorMin[1]);
                    rt.anchorMax = new Vector2(vci_rt.anchorMax[0], vci_rt.anchorMax[1]);
                    rt.anchoredPosition = new Vector2(vci_rt.anchoredPosition[0], vci_rt.anchoredPosition[1]);
                    rt.sizeDelta = new Vector2(vci_rt.sizeDelta[0], vci_rt.sizeDelta[1]);
                    rt.pivot = new Vector2(vci_rt.pivot[0], vci_rt.pivot[1]);

                    AddText(tmp);
                }
            }
        }

        public void SetupSpringBone()
        {
            if (GLTF.extensions.VCAST_vci_spring_bone == null) return;

            foreach (var bone in GLTF.extensions.VCAST_vci_spring_bone.springBones)
            {
                var sb = Root.AddComponent<VCISpringBone>();
                if(bone.center >= 0) sb.m_center = Nodes[bone.center];
                sb.m_dragForce = bone.dragForce;
                sb.m_gravityDir = bone.gravityDir;
                sb.m_gravityPower = bone.gravityPower;
                sb.m_hitRadius = bone.hitRadius;
                sb.m_stiffnessForce = bone.stiffiness;
                if (bone.colliderIds != null && bone.colliderIds.Any())
                    sb.m_colliderObjects = bone.colliderIds.Select(id => Nodes[id]).ToList();
                sb.RootBones = bone.bones.Select(x => Nodes[x]).ToList();
            }
        }

        public void SetupPlayerSpawnPoint()
        {
            for (var i = 0; i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];

                if (node.extensions != null && node.extensions.VCAST_vci_player_spawn_point != null)
                {
                    var go = Nodes[i].gameObject;
                    var spawnPoint = go.AddComponent<VCIPlayerSpawnPoint>();
                    spawnPoint.Order = node.extensions.VCAST_vci_player_spawn_point.playerSpawnPoint.order;
                    spawnPoint.Radius = node.extensions.VCAST_vci_player_spawn_point.playerSpawnPoint.radius;

                    if (node.extensions.VCAST_vci_player_spawn_point_restriction != null)
                    {
                        var spawnPointRestriction = go.AddComponent<VCIPlayerSpawnPointRestriction>();
                        var nodePspR = node.extensions.VCAST_vci_player_spawn_point_restriction
                            .playerSpawnPointRestriction;
                        switch (nodePspR.rangeOfMovementRestriction)
                        {
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.Circle:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.Circle;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.Rectangle:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.Rectangle;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.NoLimit:
                            default:
                                spawnPointRestriction.RangeOfMovementRestriction = RangeOfMovement.NoLimit;
                                break;
                        }

                        spawnPointRestriction.LimitRadius = node.extensions.VCAST_vci_player_spawn_point_restriction
                            .playerSpawnPointRestriction.limitRadius;
                        spawnPointRestriction.LimitRectLeft = nodePspR.limitRectLeft;
                        spawnPointRestriction.LimitRectRight = nodePspR.limitRectRight;
                        spawnPointRestriction.LimitRectForward = nodePspR.limitRectForward;
                        spawnPointRestriction.LimitRectBackward = nodePspR.limitRectBackward;

                        switch (nodePspR.postureRestriction)
                        {
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.SitOn:
                                spawnPointRestriction.PostureRestriction = Posture.SitOn;
                                break;
                            case glTF_VCAST_vci_PlayerSpawnPointRestriction.NoLimit:
                            default:
                                spawnPointRestriction.PostureRestriction = Posture.NoLimit;
                                break;
                        }
                    }
                }
            }
        }

        public void SetupLocationBounds()
        {
            if (GLTF.extensions.VCAST_vci_location_bounds == null) return;

            var locationBounds = Root.AddComponent<VCILocationBounds>();
            var values = GLTF.extensions.VCAST_vci_location_bounds.LocationBounds;
            locationBounds.Bounds = new Bounds(values.bounds_center, values.bounds_size);
        }


        public void ExtractAudio(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            if (GLTF.extensions.VCAST_vci_audios == null
                || GLTF.extensions.VCAST_vci_audios.audios == null
                || GLTF.extensions.VCAST_vci_audios.audios.Count == 0)
                return;

            var prefabParentDir = prefabPath.Parent;

            // glb buffer
            var folder = prefabPath.GetAssetFolder(".Audios");

            var created = 0;
            for (var i = 0; i < GLTF.extensions.VCAST_vci_audios.audios.Count; ++i)
            {
                folder.EnsureFolder();
                var audio = GLTF.extensions.VCAST_vci_audios.audios[i];
                var bytes = GLTF.GetViewBytes(audio.bufferView);

                var audioBuffer = new byte[bytes.Count];
                Buffer.BlockCopy(bytes.Array, bytes.Offset, audioBuffer, 0, audioBuffer.Length);

                System.Text.RegularExpressions.Regex r =
                    new System.Text.RegularExpressions.Regex(
                        @"(?<type>.*)/(?<ext>.*)",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                        | System.Text.RegularExpressions.RegexOptions.Singleline);
                var match = r.Match(audio.mimeType);
                var ext = match.Groups["ext"].Value;
                if(!string.IsNullOrEmpty(ext))
                {
                    var path = string.Format("{0}/{1}.{2}", folder.Value, audio.name, ext);
                    File.WriteAllBytes(path, audioBuffer);
                    AudioAssetPathList.Add(audio.name, path);
                }

                created++;
            }

            if (created > 0) AssetDatabase.Refresh();
#endif
        }

        public void ExtractEffekseer(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            if (GLTF.extensions.Effekseer == null
                || GLTF.extensions.Effekseer.effects == null
                || GLTF.extensions.Effekseer.effects.Count == 0)
                return;

            var prefabParentDir = prefabPath.Parent;
            var folder = prefabPath.GetAssetFolder(".Effekseers");

            for (var i = 0; i < GLTF.extensions.Effekseer.effects.Count; ++i)
            {
                folder.EnsureFolder();
                var effect = GLTF.extensions.Effekseer.effects[i];
                var effectDir = string.Format("{0}/{1}", folder.Value, effect.effectName);
                SafeCreateDirectory(effectDir);
                var textureDir = string.Format("{0}/{1}", effectDir, "Textures");
                var modelDir = string.Format("{0}/{1}", effectDir, "Models");

                var body = GLTF.GetViewBytes(effect.body.bufferView).ToArray();
                var resourcePath = new Effekseer.EffekseerResourcePath();
                if (!Effekseer.EffekseerEffectAsset.ReadResourcePath(body, ref resourcePath))
                {
                    continue;
                }

                // Texture
                var writeTexturePathList = new List<string>();
                if (effect.images != null && effect.images.Any())
                {
                    for (int t = 0; t < effect.images.Count; t++)
                    {
                        var image = effect.images[t];
                        var path = resourcePath.TexturePathList[t];
                        var buffer = GLTF.GetViewBytes(image.bufferView);
                        var texturePath = string.Format("{0}/{1}", textureDir, Path.GetFileName(path));
                        SafeCreateDirectory(Path.GetDirectoryName(texturePath));

                        if (image.mimeType == glTF_Effekseer_image.MimeTypeString.Png)
                        {
                            File.WriteAllBytes(texturePath, buffer.ToArray());
                            writeTexturePathList.Add(texturePath);
                        }
                        else
                        {
                            Debug.LogError(string.Format("image format {0} is not suppported.", image.mimeType));
                        }
                    }
                }

                // Models
                if (effect.models != null && effect.models.Any())
                {
                    for (int t = 0; t < effect.models.Count; t++)
                    {
                        var model = effect.models[t];
                        var path = resourcePath.ModelPathList[t];
                        var buffer = GLTF.GetViewBytes(model.bufferView);

                        var modelPath = string.Format("{0}/{1}", modelDir, Path.GetFileName(path));
                        SafeCreateDirectory(Path.GetDirectoryName(modelPath));

                        File.WriteAllBytes(modelPath, buffer.ToArray());
                        AssetDatabase.ImportAsset(modelPath);
                    }
                }

                EditorApplication.delayCall += () =>
                {
                    // fix texture setting
                    foreach (var texturePath in writeTexturePathList)
                    {
                        AssetDatabase.ImportAsset(texturePath);
                        var textureImporter = (TextureImporter)TextureImporter.GetAtPath(texturePath);
                        if(textureImporter != null)
                        {
                            textureImporter.isReadable = true;
                            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                            textureImporter.SaveAndReimport();
                        }
                    }

                    // efk
                    var assetPath = string.Format("{0}/{1}.efk", effectDir, effect.effectName);
                    Effekseer.EffekseerEffectAsset.CreateAsset(assetPath, body);
                    var effectAsset = AssetDatabase.LoadAssetAtPath<Effekseer.EffekseerEffectAsset>(System.IO.Path.ChangeExtension(assetPath, "asset"));
                    EffekseerEffectAssets.Add(effectAsset);


                    // find assets
                    // textures
                    for(int t = 0; t < effectAsset.textureResources.Count(); t++)
                    {
                        var path = effectAsset.textureResources[t].path;
                        if (string.IsNullOrEmpty(path))
                            continue;

                        var fileName = Path.GetFileName(path);
                        if (string.IsNullOrEmpty(fileName))
                            continue;

                        Texture2D texture = UnityEditor.AssetDatabase.LoadAssetAtPath(
                            string.Format("{0}/{1}", textureDir, fileName), typeof(Texture2D)) as Texture2D;

                        if(texture != null)
                        {
                            effectAsset.textureResources[t].texture = texture;
                        }
                    }

                    // models
                    for (int t = 0; t < effectAsset.modelResources.Count(); t++)
                    {
                        var path = effectAsset.modelResources[t].path;
                        if (string.IsNullOrEmpty(path))
                            continue;

                        var fileName = Path.GetFileName(path);
                        if (string.IsNullOrEmpty(fileName))
                            continue;

                        Effekseer.EffekseerModelAsset model = UnityEditor.AssetDatabase.LoadAssetAtPath(
                            string.Format("{0}/{1}", modelDir, fileName), typeof(Effekseer.EffekseerModelAsset)) as Effekseer.EffekseerModelAsset;

                        if (model != null)
                        {
                            effectAsset.modelResources[t].asset = model;
                        }
                    }
                };
            }
#endif
        }

        public void ExtractAnimation(UnityPath prefabPath)
        {
#if UNITY_EDITOR

            if (GLTF.animations == null || GLTF.animations.Count == 0)
                return;

            var prefabParentDir = prefabPath.Parent;
            var folder = prefabPath.GetAssetFolder(".Animation");
            folder.EnsureFolder();

            var animationClips = new AnimationClip[GLTF.animations.Count];
            for (int i = 0;i < GLTF.nodes.Count; i++)
            {
                var node = GLTF.nodes[i];
                if (node.extensions == null || node.extensions.VCAST_vci_animation == null)
                    continue;

                var vciAnimation = node.extensions.VCAST_vci_animation;

                foreach (var animationReference in vciAnimation.animationReferences)
                {
                    var gltfAnimation = GLTF.animations[animationReference.animation];
                    AnimationClip clip = null;
                    if(animationClips[animationReference.animation] == null)
                    {
                        clip = AnimationImporterUtil.ImportAnimationClip(this, gltfAnimation, node);
                        animationClips[animationReference.animation] = clip;
                    }
                }
            }

            // Import root animation clips.
            for (var i = 0; i < animationClips.Length; i++)
            {
                if (animationClips[i] != null) continue;
                animationClips[i] = AnimationImporterUtil.ImportAnimationClip(this, GLTF.animations[i]);
            }

            var saveAnimationPath = new string[GLTF.animations.Count];
            for(int i = 0;i<animationClips.Count();i++)
            {
                if(animationClips[i] != null)
                {
                    var path = string.Format("{0}/{1}.{2}", folder.Value, animationClips[i].name, "asset");
                    AssetDatabase.CreateAsset(animationClips[i], path);
                    saveAnimationPath[i] = path;
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var saveAnimationClips = new AnimationClip[GLTF.animations.Count];
            for(int i = 0;i<saveAnimationPath.Count();i++)
            {
                if(!string.IsNullOrEmpty(saveAnimationPath[i]))
                {
                    saveAnimationClips[i] = AssetDatabase.LoadAssetAtPath(saveAnimationPath[i], typeof(AnimationClip)) as AnimationClip;
                }
            }

            AnimationClips = new List<AnimationClip>(saveAnimationClips);
#endif
        }

        private static DirectoryInfo SafeCreateDirectory(string path)
        {
#if UNITY_EDITOR
            try
            {
                if (Directory.Exists(path))
                {
                    return null;
                }
                var info = Directory.CreateDirectory(path);
                AssetDatabase.ImportAsset(path);
                return info;
            }
            catch(Exception ex)
            {
                throw ex;
            }

#else
            return null;
#endif
        }

#if UNITY_EDITOR
        protected override UnityPath GetAssetPath(UnityPath prefabPath, UnityEngine.Object o)
        {
            if (o is AnimationClip)
            {
                var dir = prefabPath.GetAssetFolder(".Animation");
                var assetPath = dir.Child(o.name.EscapeFilePath() + ".asset");
                return assetPath;
            }
            else
            {
                return base.GetAssetPath(prefabPath, o);
            }
        }
#endif

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

            [SerializeField] [TextArea(1, 16)] public string description;

            public string exporterVersion;
            public string specVersion;


            #region Model Data License Url

            [Header("Model Data License")] public glTF_VCAST_vci_meta.LicenseType modelDataLicenseType;
            public string modelDataOtherLicenseUrl;

            #endregion

            #region Script License Url

            [Header("Script License")] public glTF_VCAST_vci_meta.LicenseType scriptLicenseType;
            public string scriptOtherLicenseUrl;

            #endregion

            #region Script Settings

            [Header("Script Settings")] public bool scriptWriteProtected;
            public bool scriptEnableDebugging;

            public glTF_VCAST_vci_meta.ScriptFormat scriptFormat;

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

            var texture = GetTexture(meta.thumbnail);
            if (texture != null) yield return texture.ProcessCoroutine(GLTF, Storage);

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

        public static VCIImporter CreateForTest()
        {
            var root = new GameObject("root");
            var vciObject = root.AddComponent<VCIObject>();
            var item = new GameObject("item");
            item.AddComponent<VCISubItem>();
            item.transform.SetParent(root.transform);

            var importer = new VCIImporter()
            {
                Root = root,
                VCIObject = vciObject,
            };

            return importer;
        }

        public interface IFontProvider
        {
            TMP_FontAsset GetDefaultFont();
        }

        public void AddMeshWitMaterials(MeshWithMaterials meshWithMats)
        {
            Meshes.Add(meshWithMats);
        }

        List<TextMeshPro> m_texts = new List<TextMeshPro>();

        public void AddText(TextMeshPro text)
        {
            m_texts.Add(text);
        }

        public IEnumerable<TextMeshPro> GetTexts(string id)
        {
            return m_texts.Where(t => t.name.ToLower() == id.ToLower());
        }
    }
}
