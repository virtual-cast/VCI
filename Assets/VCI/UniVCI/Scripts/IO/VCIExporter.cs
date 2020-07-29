using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    public class VCIExporter : gltfExporter
    {
        protected override IMaterialExporter CreateMaterialExporter()
        {
            return new VCIMaterialExporter();
        }

        public VCIExporter(glTF gltf) : base(gltf)
        {
            gltf.extensionsUsed.Add(glTF_VCAST_vci_meta.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_animation.ExtensionName);
            gltf.extensionsUsed.Add(glTF_Effekseer.ExtensionName);
            gltf.extensionsUsed.Add(glTF_Effekseer_emitters.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_text.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_rectTransform.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_spring_bone.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_player_spawn_point.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_player_spawn_point_restriction.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_location_bounds.ExtensionName);

#if VCI_EXPORTER_USE_SPARSE
            UseSparseAccessorForBlendShape = true
#endif
        }

        public override void Export()
        {
            base.Export();

            var gltf = GLTF;
            var exporter = this;
            // var go = Copy;

            //exporter.Prepare(go);
            //exporter.Export();

            gltf.extensions.VCAST_vci_material_unity = new glTF_VCAST_vci_material_unity
            {
                materials = exporter.Materials
                    .Select(m => glTF_VCI_Material.CreateFromMaterial(m, exporter.TextureManager.Textures))
                    .ToList()
            };

            if (Copy == null) return;

            // vci interaction
            var vciObject = Copy.GetComponent<VCIObject>();
            if (vciObject != null)
            {
                // script
                if (vciObject.Scripts.Any())
                    gltf.extensions.VCAST_vci_embedded_script = new glTF_VCAST_vci_embedded_script
                    {
                        scripts = vciObject.Scripts.Select(x =>
                        {
                            int viewIndex = -1;
#if UNITY_EDITOR
                            if (!string.IsNullOrEmpty(x.filePath))
                            {
                                if(File.Exists(x.filePath))
                                {
                                    using (var resader = new StreamReader(x.filePath))
                                    {
                                        string scriptStr = resader.ReadToEnd();
                                        viewIndex = gltf.ExtendBufferAndGetViewIndex<byte>(0, Utf8String.Encoding.GetBytes(scriptStr));
                                    }
                                }
                                else
                                {
                                    Debug.LogError("script file not found. スクリプトファイルが見つかりませんでした: " + x.filePath);
                                    throw new FileNotFoundException(x.filePath);
                                }
                            }
                            else
#endif
                            {
                                viewIndex = gltf.ExtendBufferAndGetViewIndex<byte>(0, Utf8String.Encoding.GetBytes(x.source));
                            }

                            return new glTF_VCAST_vci_embedded_script_source
                            {
                                name = x.name,
                                mimeType = x.mimeType,
                                targetEngine = x.targetEngine,
                                source = viewIndex,
                            };
                        })
                        .ToList()
                    };

                var springBones = Copy.GetComponents<VCISpringBone>();
                if (springBones.Length > 0)
                {
                    var sbg = new glTF_VCAST_vci_spring_bone();
                    sbg.springBones = new List<glTF_VCAST_vci_SpringBone>();
                    gltf.extensions.VCAST_vci_spring_bone = sbg;
                    foreach (var sb in springBones)
                    {
                        sbg.springBones.Add( new glTF_VCAST_vci_SpringBone()
                        {
                            center = Nodes.IndexOf(sb.m_center),
                            dragForce = sb.m_dragForce,
                            gravityDir = sb.m_gravityDir,
                            gravityPower = sb.m_gravityPower,
                            stiffiness = sb.m_stiffnessForce,
                            hitRadius = sb.m_hitRadius,
                            colliderIds = sb.m_colliderObjects
                                .Where(x => x != null)
                                .Select(x => Nodes.IndexOf(x))
                                .ToArray(),
                            bones = sb.RootBones.Where(x => x != null).Select(x => Nodes.IndexOf(x.transform)).ToArray()
                        });
                    }
                }

                // meta
                var meta = vciObject.Meta;
                gltf.extensions.VCAST_vci_meta = new glTF_VCAST_vci_meta
                {
                    exporterVCIVersion = VCIVersion.VCI_VERSION,
                    specVersion = VCISpecVersion.Version,

                    title = meta.title,

                    version = meta.version,
                    author = meta.author,
                    contactInformation = meta.contactInformation,
                    reference = meta.reference,
                    description = meta.description,

                    modelDataLicenseType = meta.modelDataLicenseType,
                    modelDataOtherLicenseUrl = meta.modelDataOtherLicenseUrl,
                    scriptLicenseType = meta.scriptLicenseType,
                    scriptOtherLicenseUrl = meta.scriptOtherLicenseUrl,

                    scriptWriteProtected = meta.scriptWriteProtected,
                    scriptEnableDebugging = meta.scriptEnableDebugging,
                    scriptFormat = meta.scriptFormat
                };
                if (meta.thumbnail != null)
                    gltf.extensions.VCAST_vci_meta.thumbnail = TextureExporter.ExportTexture(
                        gltf, gltf.buffers.Count - 1, meta.thumbnail, glTFTextureTypes.Unknown);
            }

            // collider & rigidbody & joint & item & playerSpawnPoint
            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = gltf.nodes[i];

                // 各ノードに複数のコライダーがあり得る
                var colliders = node.GetComponents<Collider>();
                if (colliders.Any())
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_collider = new glTF_VCAST_vci_colliders();
                    gltfNode.extensions.VCAST_vci_collider.colliders = new List<glTF_VCAST_vci_Collider>();

                    foreach (var collider in colliders)
                    {
                        var gltfCollider = glTF_VCAST_vci_Collider.GetglTfColliderFromUnityCollider(collider);
                        if (gltfCollider == null)
                        {
                            Debug.LogWarningFormat("collider is not supported: {0}", collider.GetType().Name);
                            continue;
                        }

                        gltfNode.extensions.VCAST_vci_collider.colliders.Add(gltfCollider);
                    }
                }

                var rigidbodies = node.GetComponents<Rigidbody>();
                if (rigidbodies.Any())
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_rigidbody = new glTF_VCAST_vci_rigidbody();
                    gltfNode.extensions.VCAST_vci_rigidbody.rigidbodies = new List<glTF_VCAST_vci_Rigidbody>();

                    foreach (var rigidbody in rigidbodies)
                        gltfNode.extensions.VCAST_vci_rigidbody.rigidbodies.Add(
                            glTF_VCAST_vci_Rigidbody.GetglTfRigidbodyFromUnityRigidbody(rigidbody));
                }

                var joints = node.GetComponents<Joint>();
                if (joints.Any())
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_joints = new glTF_VCAST_vci_joints();
                    gltfNode.extensions.VCAST_vci_joints.joints = new List<glTF_VCAST_vci_joint>();

                    foreach (var joint in joints)
                        gltfNode.extensions.VCAST_vci_joints.joints.Add(
                            glTF_VCAST_vci_joint.GetglTFJointFromUnityJoint(joint, exporter.Nodes));
                }

                var item = node.GetComponent<VCISubItem>();
                if (item != null)
                {
                    var warning = item.ExportWarning;
                    if (!string.IsNullOrEmpty(warning)) throw new System.Exception(warning);

                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_item = new glTF_VCAST_vci_item
                    {
                        grabbable = item.Grabbable,
                        scalable = item.Scalable,
                        uniformScaling = item.UniformScaling,
                        groupId = item.GroupId,
                    };
                }

                // Attachable
                var vciAttachable = node.GetComponent<VCIAttachable>();
                if(vciAttachable != null
                    && vciAttachable.AttachableHumanBodyBones != null
                    && vciAttachable.AttachableHumanBodyBones.Any())
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_attachable = new glTF_VCAST_vci_attachable
                    {
                        attachableHumanBodyBones = vciAttachable.AttachableHumanBodyBones.Select(x => x.ToString()).ToList(),
                        attachableDistance = vciAttachable.AttachableDistance,
                        scalable = vciAttachable.Scalable,
                        offset = vciAttachable.Offset
                    };
                }

                // Text
                var tmp = node.GetComponent<TextMeshPro>();
                var rt = node.GetComponent<RectTransform>();
                if (tmp != null && rt != null)
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_rectTransform = new glTF_VCAST_vci_rectTransform()
                    {
                        rectTransform = glTF_VCAST_vci_RectTransform.CreateFromRectTransform(rt)
                    };

                    gltfNode.extensions.VCAST_vci_text = new glTF_VCAST_vci_text
                    {
                        text = glTF_VCAST_vci_Text.Create(tmp)
                    };
                }

                // PlayerSpawnPoint
                var psp = node.GetComponent<VCIPlayerSpawnPoint>();
                if (psp != null)
                {
                    if (gltfNode.extensions == null) gltfNode.extensions = new glTFNode_extensions();

                    gltfNode.extensions.VCAST_vci_player_spawn_point = new glTF_VCAST_vci_player_spawn_point
                    {
                        playerSpawnPoint = glTF_VCAST_vci_PlayerSpawnPoint.Create(psp)
                    };

                    var pspR = node.GetComponent<VCIPlayerSpawnPointRestriction>();
                    if (pspR != null)
                    {
                        gltfNode.extensions.VCAST_vci_player_spawn_point_restriction = new glTF_VCAST_vci_player_spawn_point_restriction
                        {
                            playerSpawnPointRestriction = glTF_VCAST_vci_PlayerSpawnPointRestriction.Create(pspR)
                        };
                    }
                }
            }

            // Audio
            var clips = exporter.Copy.GetComponentsInChildren<AudioSource>()
                .Select(x => x.clip)
                .Where(x => x != null)
                .ToArray();
            if (clips.Any())
            {
                var audios = clips.Select(x => FromAudioClip(gltf, x)).Where(x => x != null).ToList();
                gltf.extensions.VCAST_vci_audios = new glTF_VCAST_vci_audios
                {
                    audios = audios
                };
            }

#if UNITY_EDITOR
            // Animation
            // None RootAnimation
            var animators = exporter.Copy.GetComponentsInChildren<Animator>().Where(x => exporter.Copy != x.gameObject);
            var animations = exporter.Copy.GetComponentsInChildren<Animation>().Where(x => exporter.Copy != x.gameObject);
            // NodeIndex to AnimationClips
            Dictionary<int, AnimationClip[]> animationNodeList = new Dictionary<int, AnimationClip[]>();

            foreach(var animator in animators)
            {
                var animationClips = AnimationExporter.GetAnimationClips(animator);
                var nodeIndex = exporter.Nodes.FindIndex(0, exporter.Nodes.Count, x => x == animator.transform);
                if(animationClips.Any() && nodeIndex != -1)
                {
                    animationNodeList.Add(nodeIndex, animationClips.ToArray());
                }
            }

            foreach (var animation in animations)
            {
                var animationClips = AnimationExporter.GetAnimationClips(animation);
                var nodeIndex = exporter.Nodes.FindIndex(0, exporter.Nodes.Count, x => x == animation.transform);
                if (animationClips.Any() && nodeIndex != -1)
                {
                    animationNodeList.Add(nodeIndex, animationClips.ToArray());
                }
            }

            int bufferIndex = 0;
            foreach (var animationNode in animationNodeList)
            {
                List<int> clipIndices = new List<int>();
                // write animationClips
                foreach (var clip in animationNode.Value)
                {
                    var animationWithCurve = AnimationExporter.Export(clip, Nodes[animationNode.Key], Nodes);
                    AnimationExporter.WriteAnimationWithSampleCurves(gltf, animationWithCurve, clip.name, bufferIndex);
                    clipIndices.Add(gltf.animations.IndexOf(animationWithCurve.Animation));
                }

                // write node
                if(clipIndices.Any())
                {
                    var node = gltf.nodes[animationNode.Key];
                    if (node.extensions == null)
                        node.extensions = new glTFNode_extensions();

                    node.extensions.VCAST_vci_animation = new glTF_VCAST_vci_animation()
                    {
                        animationReferences = new List<glTF_VCAST_vci_animationReference>()
                    };

                    foreach(var index in clipIndices)
                    {
                        node.extensions.VCAST_vci_animation.animationReferences.Add(new glTF_VCAST_vci_animationReference() { animation = index });
                    }
                }
            }
#endif

            // Effekseer
            var effekseerEmitters = exporter.Copy.GetComponentsInChildren<Effekseer.EffekseerEmitter>()
                .Where(x => x.effectAsset != null)
                .ToArray();

            if(effekseerEmitters.Any())
            {
                gltf.extensions.Effekseer = new glTF_Effekseer()
                {
                    effects = new List<glTF_Effekseer_effect>()
                };

                foreach (var emitter in effekseerEmitters)
                {
                    var index = exporter.Nodes.FindIndex(x => x == emitter.transform);
                    if(index < 0)
                    {
                        continue;
                    }

                    var effectIndex = AddEffekseerEffect(gltf, emitter);
                    var gltfNode = gltf.nodes[index];
                    if(gltfNode.extensions == null)
                    {
                        gltfNode.extensions = new glTFNode_extensions();
                    }
                    if(gltfNode.extensions.Effekseer_emitters == null)
                    {
                        gltfNode.extensions.Effekseer_emitters = new glTF_Effekseer_emitters() { emitters = new List<glTF_Effekseer_emitter>() };
                    }

                    gltfNode.extensions.Effekseer_emitters.emitters.Add(new glTF_Effekseer_emitter() {
                        effectIndex = effectIndex,
                        isLoop = emitter.isLooping,
                        isPlayOnStart = emitter.playOnStart
                    });
                }
            }

            // LocationBounds
            var locationBounds = exporter.Copy.GetComponent<VCILocationBounds>();
            if (locationBounds != null)
            {
                gltf.extensions.VCAST_vci_location_bounds = new glTF_VCAST_vci_location_bounds
                {
                    LocationBounds = glTF_VCAST_vci_LocationBounds.Create(locationBounds)
                };
            }
        }


        private static glTF_VCAST_vci_audio FromAudioClip(glTF gltf, AudioClip clip)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
#endif
            {
                var bytes = WaveUtil.GetWaveBinary(clip);
                var viewIndex = gltf.ExtendBufferAndGetViewIndex(0, bytes);
                return new glTF_VCAST_vci_audio
                {
                    name = clip.name,
                    mimeType = "audio/wav",
                    bufferView = viewIndex,
                };
            }
#if UNITY_EDITOR
            else
            {
                var path = UnityPath.FromAsset(clip);
                if (!path.IsUnderAssetsFolder) return null;
                if (path.Extension.ToLower() == ".wav")
                {
                    var bytes = File.ReadAllBytes(path.FullPath);
                    var viewIndex = gltf.ExtendBufferAndGetViewIndex(0, bytes);
                    return new glTF_VCAST_vci_audio
                    {
                        name = clip.name,
                        mimeType = "audio/wav",
                        bufferView = viewIndex,
                    };
                }
                else if (path.Extension.ToLower() == ".mp3")
                {
                    var bytes = File.ReadAllBytes(path.FullPath);
                    var viewIndex = gltf.ExtendBufferAndGetViewIndex(0, bytes);
                    return new glTF_VCAST_vci_audio
                    {
                        name = clip.name,
                        mimeType = "audio/mp3",
                        bufferView = viewIndex,
                    };
                }
                else
                {
                    // Convert to wav
                    var bytes = WaveUtil.GetWaveBinary(clip);
                    var viewIndex = gltf.ExtendBufferAndGetViewIndex(0, bytes);
                    return new glTF_VCAST_vci_audio
                    {
                        name = clip.name,
                        mimeType = "audio/wav",
                        bufferView = viewIndex,
                    };
                }
            }
#endif
        }

        private int AddEffekseerEffect(glTF gltf, Effekseer.EffekseerEmitter emitter)
        {
            if(gltf.extensions.Effekseer.effects.FirstOrDefault(x => x.effectName == emitter.effectAsset.name) == null)
            {
                var viewIndex = gltf.ExtendBufferAndGetViewIndex(0, emitter.effectAsset.efkBytes);

                // body
                var effect = new glTF_Effekseer_effect()
                {
                    nodeIndex = 0,
                    nodeName = "Root",
                    effectName = emitter.effectAsset.name,
                    scale = emitter.effectAsset.Scale,
                    body = new glTF_Effekseer_body() { bufferView = viewIndex },
                    images = new List<glTF_Effekseer_image>(),
                    models = new List<glTF_Effekseer_model>()
                };

                // texture
                foreach (var texture in emitter.effectAsset.textureResources)
                {
                    if(texture == null || texture.texture == null)
                    {
                        Debug.LogWarning("Effekseer Texture Asset is null. " + texture?.path);
                        continue;
                    }
#if UNITY_EDITOR
                    var texturePath = UnityEditor.AssetDatabase.GetAssetPath(texture.texture);
                    var textureImporter = (UnityEditor.TextureImporter)UnityEditor.TextureImporter.GetAtPath(texturePath);
                    if(textureImporter != null)
                    {
                        textureImporter.isReadable = true;
                        textureImporter.textureCompression = UnityEditor.TextureImporterCompression.Uncompressed;
                        textureImporter.SaveAndReimport();
                    }

#endif
                    var textureBytes = TextureExporter.GetBytesWithMime(texture.texture, glTFTextureTypes.Unknown);
                    var image = new glTF_Effekseer_image()
                    {

                        bufferView = gltf.ExtendBufferAndGetViewIndex(0, textureBytes.bytes),
                        mimeType = textureBytes.mine
                    };
                    effect.images.Add(image);
                }

                // model
                foreach (var model in emitter.effectAsset.modelResources)
                {
                    if(model == null || model.asset == null)
                    {
                        Debug.LogWarning("Effekseer Model Asset is null. " + model?.path);
                        continue;
                    }

                    var efkModel = new glTF_Effekseer_model()
                    {
                        bufferView = gltf.ExtendBufferAndGetViewIndex(0, model.asset.bytes)
                    };
                    effect.models.Add(efkModel);
                }

                gltf.extensions.Effekseer.effects.Add(effect);
                int index = gltf.extensions.Effekseer.effects.Count - 1;
                return index;
            }
            else
            {
                return gltf.extensions.Effekseer.effects.FindIndex(x => x.effectName == emitter.effectAsset.name);
            }
        }
    }
}
