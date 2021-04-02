using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UniGLTF;
using UniJSON;
using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{

    public class VCIExporter : gltfExporter
    {

        private List<Transform> _originalNodes = new List<Transform>();

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
            gltf.extensionsUsed.Add(glTF_VCAST_vci_location_lighting.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_lightmap.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_reflectionProbe.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_materials_pbr.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_audios.ExtensionName);
            gltf.extensionsUsed.Add(glTF_VCAST_vci_audio_sources.ExtensionName);

#if VCI_EXPORTER_USE_SPARSE
            UseSparseAccessorForBlendShape = true
#endif
        }

        public override void Prepare(GameObject go)
        {
            base.Prepare(go);

            // base.Export() と同じ方法で Nodes を構築し、インデックスで対応して参照できるようにする。
            _originalNodes = go.transform.Traverse().Skip(go.transform.childCount == 0 ? 0 : 1).ToList();
        }

        public override void Export(MeshExportSettings configuration, Func<Texture, bool> useAsset)
        {
            base.Export(configuration, useAsset);

            var gltf = glTF;
            var exporter = this;

            // VCIのmaterial拡張
            {
                var VCAST_vci_material_unity = new glTF_VCAST_vci_material_unity();
                VCAST_vci_material_unity.materials = new List<glTF_VCI_Material>();
                foreach (var material in exporter.Materials)
                {
                    VCAST_vci_material_unity.materials.Add(VCIMaterialExporter.CreateFromMaterial(material, TextureManager.GetTextureIndex));
                }

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_material_unity_Serializer.Serialize(f, VCAST_vci_material_unity);
                glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_material_unity.ExtensionName, f.GetStore().Bytes);
            }


            if (Copy == null) return;

            // vci interaction
            var vciObject = Copy.GetComponent<VCIObject>();
            if (vciObject != null)
            {
                // script
                if (vciObject.Scripts.Any())
                {
                    var VCAST_vci_embedded_script = new glTF_VCAST_vci_embedded_script
                    {
                        scripts = vciObject.Scripts.Select(x =>
                            {
                                int viewIndex = -1;
#if UNITY_EDITOR
                                if (x.textAsset)
                                {
                                    viewIndex = gltf.ExtendBufferAndGetViewIndex<byte>(0,
                                        Utf8String.Encoding.GetBytes(x.textAsset.text));
                                }
                                else
#endif
                                {
                                    viewIndex = gltf.ExtendBufferAndGetViewIndex<byte>(0,
                                        Utf8String.Encoding.GetBytes(x.source));
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

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_embedded_script_Serializer.Serialize(f, VCAST_vci_embedded_script);
                    glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_embedded_script.ExtensionName, f.GetStore().Bytes);
                }

                var springBones = Copy.GetComponents<VCISpringBone>();
                if (springBones.Length > 0)
                {
                    var VCAST_vci_spring_bone = new glTF_VCAST_vci_spring_bone();
                    VCAST_vci_spring_bone.springBones = new List<glTF_VCAST_vci_SpringBone>();
                    foreach (var sb in springBones)
                    {
                        VCAST_vci_spring_bone.springBones.Add(new glTF_VCAST_vci_SpringBone()
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

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_spring_bone_Serializer.Serialize(f, VCAST_vci_spring_bone);
                    glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_spring_bone.ExtensionName, f.GetStore().Bytes);
                }

                // meta
                {
                    var meta = vciObject.Meta;
                    var VCAST_vci_meta = new glTF_VCAST_vci_meta
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
                    {
                        VCAST_vci_meta.thumbnail = TextureManager.ExportSRGB(meta.thumbnail);
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_meta_Serializer.Serialize(f, VCAST_vci_meta);
                    glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_meta.ExtensionName, f.GetStore().Bytes);
                }
            }

            // Audio
            var clips = exporter.Copy.GetComponentsInChildren<AudioSource>()
                .Select(x => x.clip)
                .Where(x => x != null)
                .ToArray();
            glTF_VCAST_vci_audios VCAST_vci_audios = null;
            if (clips.Any())
            {
                var audios = new List<glTF_VCAST_vci_audio>();
                foreach (var clip in clips)
                {
                    if (audios.Exists(x => x.name == clip.name))
                    {
                        continue;
                    }

                    var audio = FromAudioClip(gltf, clip);
                    if (audio != null)
                    {
                        audios.Add(audio);
                    }
                }

                VCAST_vci_audios = new glTF_VCAST_vci_audios
                {
                    audios = audios
                };

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_audios_Serializer.Serialize(f, VCAST_vci_audios);
                glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_audios.ExtensionName, f.GetStore().Bytes);
            }

            // collider & rigidbody & joint & item & playerSpawnPoint & audioSource
            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = gltf.nodes[i];

                // 各ノードに複数のコライダーがあり得る
                var colliders = node.GetComponents<Collider>();
                if (colliders.Any())
                {
                    var VCAST_vci_collider = new glTF_VCAST_vci_colliders();
                    VCAST_vci_collider.colliders = new List<glTF_VCAST_vci_Collider>();

                    foreach (var collider in colliders)
                    {
                        var gltfCollider = glTF_VCAST_vci_Collider.GetglTfColliderFromUnityCollider(collider);
                        if (gltfCollider == null)
                        {
                            Debug.LogWarningFormat("collider is not supported: {0}", collider.GetType().Name);
                            continue;
                        }

                        if (VciColliderSetting.TryGetVciLayerLabel(node.gameObject.layer, out var label))
                        {
                            if (!string.IsNullOrEmpty(label))
                            {
                                gltfCollider.layer = label;
                            }
                        }

                        VCAST_vci_collider.colliders.Add(gltfCollider);
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_colliders_Serializer.Serialize(f, VCAST_vci_collider);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_colliders.ExtensionName, f.GetStore().Bytes);
                }

                var rigidbodies = node.GetComponents<Rigidbody>();
                if (rigidbodies.Any())
                {
                    var VCAST_vci_rigidbody = new glTF_VCAST_vci_rigidbody();
                    VCAST_vci_rigidbody.rigidbodies = new List<glTF_VCAST_vci_Rigidbody>();

                    foreach (var rigidbody in rigidbodies)
                    {
                        VCAST_vci_rigidbody.rigidbodies.Add(
                            glTF_VCAST_vci_Rigidbody.GetglTfRigidbodyFromUnityRigidbody(rigidbody));
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_rigidbody_Serializer.Serialize(f, VCAST_vci_rigidbody);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_rigidbody.ExtensionName, f.GetStore().Bytes);
                }

                var joints = node.GetComponents<Joint>();
                if (joints.Any())
                {
                    var VCAST_vci_joints = new glTF_VCAST_vci_joints();
                    VCAST_vci_joints.joints = new List<glTF_VCAST_vci_joint>();

                    foreach (var joint in joints)
                    {
                        VCAST_vci_joints.joints.Add(glTF_VCAST_vci_joint.GetglTFJointFromUnityJoint(joint, exporter.Nodes));
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_joints_Serializer.Serialize(f, VCAST_vci_joints);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_joints.ExtensionName, f.GetStore().Bytes);
                }

                var item = node.GetComponent<VCISubItem>();
                if (item != null)
                {
                    var warning = item.ExportWarning;
                    if (!string.IsNullOrEmpty(warning)) throw new System.Exception(warning);

                    var VCAST_vci_item = new glTF_VCAST_vci_item
                    {
                        grabbable = item.Grabbable,
                        scalable = item.Scalable,
                        uniformScaling = item.UniformScaling,
                        attractable = item.Attractable,
                        groupId = item.GroupId,
                    };

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_item_Serializer.Serialize(f, VCAST_vci_item);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_item.ExtensionName, f.GetStore().Bytes);
                }

                // Attachable
                var vciAttachable = node.GetComponent<VCIAttachable>();
                if (vciAttachable != null
                    && vciAttachable.AttachableHumanBodyBones != null
                    && vciAttachable.AttachableHumanBodyBones.Any())
                {
                    var VCAST_vci_attachable = new glTF_VCAST_vci_attachable
                    {
                        attachableHumanBodyBones =
                            vciAttachable.AttachableHumanBodyBones.Select(x => x.ToString()).ToList(),
                        attachableDistance = vciAttachable.AttachableDistance,
                        scalable = vciAttachable.Scalable,
                        offset = vciAttachable.Offset
                    };

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_attachable_Serializer.Serialize(f, VCAST_vci_attachable);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_attachable.ExtensionName, f.GetStore().Bytes);
                }

                // Text
                var tmp = node.GetComponent<TextMeshPro>();
                var rt = node.GetComponent<RectTransform>();
                if (tmp != null && rt != null)
                {
                    {
                        var VCAST_vci_rectTransform = new glTF_VCAST_vci_rectTransform()
                        {
                            rectTransform = glTF_VCAST_vci_RectTransform.CreateFromRectTransform(rt)
                        };

                        var f = new UniJSON.JsonFormatter();
                        glTF_VCAST_vci_rectTransform_Serializer.Serialize(f, VCAST_vci_rectTransform);
                        glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_rectTransform.ExtensionName, f.GetStore().Bytes);
                    }

                    {
                        var VCAST_vci_text = new glTF_VCAST_vci_text
                        {
                            text = glTF_VCAST_vci_Text.Create(tmp)
                        };

                        var f = new UniJSON.JsonFormatter();
                        glTF_VCAST_vci_text_Serializer.Serialize(f, VCAST_vci_text);
                        glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_text.ExtensionName, f.GetStore().Bytes);
                    }

                }

                // PlayerSpawnPoint
                var psp = node.GetComponent<VCIPlayerSpawnPoint>();
                if (psp != null)
                {
                    {
                        var VCAST_vci_player_spawn_point = new glTF_VCAST_vci_player_spawn_point
                        {
                            playerSpawnPoint = glTF_VCAST_vci_PlayerSpawnPoint.Create(psp)
                        };

                        var f = new UniJSON.JsonFormatter();
                        glTF_VCAST_vci_player_spawn_point_Serializer.Serialize(f, VCAST_vci_player_spawn_point);
                        glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_player_spawn_point.ExtensionName, f.GetStore().Bytes);
                    }

                    var pspR = node.GetComponent<VCIPlayerSpawnPointRestriction>();
                    if (pspR != null)
                    {
                        var VCAST_vci_player_spawn_point_restriction = new glTF_VCAST_vci_player_spawn_point_restriction
                        {
                            playerSpawnPointRestriction = glTF_VCAST_vci_PlayerSpawnPointRestriction.Create(pspR)
                        };

                        var f = new UniJSON.JsonFormatter();
                        glTF_VCAST_vci_player_spawn_point_restriction_Serializer.Serialize(f, VCAST_vci_player_spawn_point_restriction);
                        glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_player_spawn_point_restriction.ExtensionName, f.GetStore().Bytes);
                    }
                }

                var audioSources = node.GetComponents<AudioSource>()
                    .Where(audioSource => audioSource.clip != null)
                    .ToArray();

                if (audioSources.Any())
                {
                    var VCAST_vci_audio_sources = new glTF_VCAST_vci_audio_sources
                    {
                        audioSources = new List<glTF_VCAST_vci_audio_source>()
                    };

                    foreach (var audioSource in audioSources)
                    {
                        VCAST_vci_audio_sources.audioSources.Add(
                            glTF_VCAST_vci_audio_source.CreateFrom(audioSource, VCAST_vci_audios));
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_audio_sources_Serializer.Serialize(f, VCAST_vci_audio_sources);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_audio_sources.ExtensionName, f.GetStore().Bytes);
                }
            }

#if UNITY_EDITOR
            // Animation
            // None RootAnimation
            var animators = exporter.Copy.GetComponentsInChildren<Animator>().Where(x => exporter.Copy != x.gameObject);
            var animations = exporter.Copy.GetComponentsInChildren<Animation>()
                .Where(x => exporter.Copy != x.gameObject);
            // NodeIndex to AnimationClips
            Dictionary<int, AnimationClip[]> animationNodeList = new Dictionary<int, AnimationClip[]>();

            foreach (var animator in animators)
            {
                var animationClips = AnimationExporter.GetAnimationClips(animator);
                var nodeIndex = exporter.Nodes.FindIndex(0, exporter.Nodes.Count, x => x == animator.transform);
                if (animationClips.Any() && nodeIndex != -1)
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
                    VciAnimationExporter.WriteAnimationWithSampleCurves(gltf, animationWithCurve, clip.name, bufferIndex);
                    clipIndices.Add(gltf.animations.IndexOf(animationWithCurve.Animation));
                }

                // write node
                if (clipIndices.Any())
                {
                    var node = gltf.nodes[animationNode.Key];

                    var VCAST_vci_animation = new glTF_VCAST_vci_animation()
                    {
                        animationReferences = new List<glTF_VCAST_vci_animationReference>()
                    };

                    foreach (var index in clipIndices)
                    {
                        VCAST_vci_animation.animationReferences.Add(
                            new glTF_VCAST_vci_animationReference() { animation = index });
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_VCAST_vci_animation_Serializer.Serialize(f, VCAST_vci_animation);
                    glTFExtensionExport.GetOrCreate(ref node.extensions).Add(glTF_VCAST_vci_animation.ExtensionName, f.GetStore().Bytes);
                }
            }
#endif

            // Effekseer
            var effekseerExtensions = new glTF_Effekseer()
            {
                effects = new List<glTF_Effekseer_effect>()
            };

            // Effekseer emitter
            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = gltf.nodes[i];

                var emitters = node.GetComponents<Effekseer.EffekseerEmitter>();

                if (emitters != null && emitters.Length > 0)
                {
                    var Effekseer_emitters = new glTF_Effekseer_emitters()
                    {
                        emitters = new List<glTF_Effekseer_emitter>()
                    };

                    foreach (var emitter in emitters)
                    {
                        var effectIndex = AddEffekseerEffect(gltf, effekseerExtensions, emitter);
                        Effekseer_emitters.emitters.Add(new glTF_Effekseer_emitter()
                        {
                            effectIndex = effectIndex,
                            isLoop = emitter.isLooping,
                            isPlayOnStart = emitter.playOnStart
                        });
                    }

                    var f = new UniJSON.JsonFormatter();
                    glTF_Effekseer_emitters_Serializer.Serialize(f, Effekseer_emitters);
                    glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_Effekseer_emitters.ExtensionName, f.GetStore().Bytes);
                }
            }

            // Effekseer extension
            if (effekseerExtensions.effects != null && effekseerExtensions.effects.Count() > 0)
            {
                var f = new UniJSON.JsonFormatter();
                glTF_Effekseer_Serializer.Serialize(f, effekseerExtensions);
                glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_Effekseer.ExtensionName, f.GetStore().Bytes);
            }


            // LocationBounds
            var locationBounds = exporter.Copy.GetComponent<VCILocationBounds>();
            if (locationBounds != null)
            {
                var VCAST_vci_location_bounds = new glTF_VCAST_vci_location_bounds
                {
                    LocationBounds = glTF_VCAST_vci_LocationBounds.Create(locationBounds)
                };

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_location_bounds_Serializer.Serialize(f, VCAST_vci_location_bounds);
                glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_location_bounds.ExtensionName, f.GetStore().Bytes);
            }

            // Scene Lighting
#if VCI_DEVELOP
            ExportSceneLighting(exporter, gltf);
#endif

            // Extension で Texture が増える場合があるので最後に呼ぶ
            for (int i = 0; i < TextureManager.Exported.Count; ++i)
            {
                var unityTexture = TextureManager.Exported[i];
                glTF.PushGltfTexture(bufferIndex, unityTexture);
            }
        }

        private void ExportSceneLighting(VCIExporter exporter, glTF gltf)
        {
            var lightmapTextureExporter = new LightmapTextureExporter(TextureManager, gltf);

            var existsLightmappedMesh = false;
            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = gltf.nodes[i];

                var renderer = node.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    var useLightmapExtension = false;
#if UNITY_EDITOR
                    var contributeGi = UnityEditor.GameObjectUtility.GetStaticEditorFlags(node.gameObject)
                        .HasFlag(UnityEditor.StaticEditorFlags.ContributeGI);
                    var receiveLightmap = renderer.receiveGI == ReceiveGI.Lightmaps;
                    var isLightmapExistsInScene =
                        LightmapSettings.lightmaps != null && LightmapSettings.lightmaps.Length > 0;
                    useLightmapExtension = contributeGi && receiveLightmap && isLightmapExistsInScene;
#endif

                    if (useLightmapExtension)
                    {
                        var originalRenderer = _originalNodes[i].GetComponent<MeshRenderer>();

                        var lightmapUnityIndex = originalRenderer.lightmapIndex;
                        if (lightmapUnityIndex < 0 || lightmapUnityIndex >= LightmapSettings.lightmaps.Length) continue;

                        var lightmapGltfIndex = lightmapTextureExporter.GetOrAddColorTexture(lightmapUnityIndex);

                        if (lightmapGltfIndex >= 0)
                        {
                            var so = originalRenderer.lightmapScaleOffset;
                            var scale = new Vector2(so.x, so.y);
                            var offset = new Vector2(so.z, so.w);
                            offset.y = (offset.y + scale.y - 1) * -1.0f;

                            var VCAST_vci_lightmap = new glTF_VCAST_vci_lightmap
                            {
                                lightmap = new glTF_VCAST_vci_Lightmap
                                {
                                    texture = new glTFLightmapTextureInfo { index = lightmapGltfIndex },
                                    offset = new[] { offset.x, offset.y },
                                    scale = new[] { scale.x, scale.y },
                                },
                            };

                            var f = new UniJSON.JsonFormatter();
                            glTF_VCAST_vci_lightmap_Serializer.Serialize(f, VCAST_vci_lightmap);
                            glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_lightmap.ExtensionName, f.GetStore().Bytes);

                            existsLightmappedMesh = true;
                        }
                    }
                }
            }

            var enableLocationLightingExtension = existsLightmappedMesh;
            if (enableLocationLightingExtension)
            {
                var cubemapExporter = new CubemapTextureExporter(TextureManager, glTF);
                var skyboxExporter = new SkyboxExporter(cubemapExporter);
                var lightProbeExporter = new LightProbeExporter();

                var VCAST_vci_location_lighting = new glTF_VCAST_vci_location_lighting
                {
                    locationLighting = new glTF_VCAST_vci_LocationLighting
                    {
                        lightmapCompressionMode =
                            glTF_VCAST_vci_LocationLighting.ConvertLightmapCompressionMode(lightmapTextureExporter
                                .CompressionType),
                        lightmapDirectionalMode =
                            glTF_VCAST_vci_LocationLighting.ConvertLightmapDirectionalMode(lightmapTextureExporter
                                .DirectionalType),
                        lightmapTextures = lightmapTextureExporter.RegisteredColorTextureIndexArray
                            .Select(x => new glTFLightmapTextureInfo { index = x })
                            .ToArray(),
                        skyboxCubemap = skyboxExporter.Export(1024),
                        lightProbes = lightProbeExporter.Export(),
                    },
                };

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_location_lighting_Serializer.Serialize(f, VCAST_vci_location_lighting);
                glTFExtensionExport.GetOrCreate(ref gltf.extensions).Add(glTF_VCAST_vci_location_lighting.ExtensionName, f.GetStore().Bytes);
            }

            for (var i = 0; i < exporter.Nodes.Count; i++)
            {
                var node = exporter.Nodes[i];
                var gltfNode = gltf.nodes[i];

                var reflectionProbe = _originalNodes[i].GetComponent<ReflectionProbe>();
                if (reflectionProbe == null) continue;

                var exportReflectionProbeExtension = false;
                var isModeActive = reflectionProbe.mode == ReflectionProbeMode.Baked;
                var texture = reflectionProbe.bakedTexture;
                var isTextureExists = texture != null && texture.dimension == TextureDimension.Cube;

                exportReflectionProbeExtension = !Application.isPlaying && isModeActive && isTextureExists;
                if (!exportReflectionProbeExtension) continue;

                var reflectionProbeCubemapExporter = new CubemapTextureExporter(TextureManager, glTF);

                var offset = reflectionProbe.center;
                var size = reflectionProbe.size;

                var VCAST_vci_reflectionProbe = new glTF_VCAST_vci_reflectionProbe
                {
                    reflectionProbe = new glTF_VCAST_vci_ReflectionProbe
                    {
                        boxOffset = new[] { -offset.x, offset.y, offset.z }, // invert X-axis
                        boxSize = new[] { size.x, size.y, size.z },
                        intensity = reflectionProbe.intensity,
                        useBoxProjection = reflectionProbe.boxProjection,
                        cubemap = reflectionProbeCubemapExporter.Export(texture, reflectionProbe.resolution,
                            includeMipmaps: true),
                    },
                };

                var f = new UniJSON.JsonFormatter();
                glTF_VCAST_vci_reflectionProbe_Serializer.Serialize(f, VCAST_vci_reflectionProbe);
                glTFExtensionExport.GetOrCreate(ref gltfNode.extensions).Add(glTF_VCAST_vci_reflectionProbe.ExtensionName, f.GetStore().Bytes);
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

        private int AddEffekseerEffect(glTF gltf, glTF_Effekseer effekseer, Effekseer.EffekseerEmitter emitter)
        {
            if (effekseer.effects.FirstOrDefault(x => x.effectName == emitter.effectAsset.name) == null)
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
                    if (texture == null || texture.texture == null)
                    {
                        Debug.LogWarning("Effekseer Texture Asset is null. " + texture?.path);
                        continue;
                    }
#if UNITY_EDITOR
                    var texturePath = UnityEditor.AssetDatabase.GetAssetPath(texture.texture);
                    var textureImporter =
                        (UnityEditor.TextureImporter)UnityEditor.TextureImporter.GetAtPath(texturePath);
                    if (textureImporter != null)
                    {
                        textureImporter.isReadable = true;
                        textureImporter.textureCompression = UnityEditor.TextureImporterCompression.Uncompressed;
                        textureImporter.SaveAndReimport();
                    }

#endif
                    var textureBytes = GltfTextureExporter.GetBytesWithMime(texture.texture);
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
                    if (model == null || model.asset == null)
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

                effekseer.effects.Add(effect);
                int index = effekseer.effects.Count - 1;
                return index;
            }
            else
            {
                return effekseer.effects.FindIndex(x => x.effectName == emitter.effectAsset.name);
            }
        }
    }
}