using System;
using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UniJSON;
using UnityEngine;

namespace VCI
{
    public sealed class VciBinaryParser
    {
        private readonly byte[] _data;

        public VciBinaryParser(byte[] data)
        {
            _data = data;
        }

        public VciData Parse()
        {
            var gltfData = new GlbLowLevelParser(string.Empty, _data).Parse();

            return new VciData(gltfData,
                CheckMigrationFlags(gltfData),
                DeserializeScriptExtension(gltfData.GLTF),
                DeserializeAudioExtension(gltfData.GLTF),
                DeserializeUnityMaterialExtension(gltfData.GLTF),
                DeserializeLocationLightingExtension(gltfData.GLTF),
                DeserializeSpringBoneExtension(gltfData.GLTF),
                DeserializeEffekseerExtension(gltfData.GLTF),
                DeserializeAudioSourcesNodeExtensions(gltfData.GLTF),
                DeserializeAnimationNodeExtensions(gltfData.GLTF),
                DeserializeAttachableExtensions(gltfData.GLTF),
                DeserializeLightmapExtensions(gltfData.GLTF),
                DeserializeReflectionProbeExtensions(gltfData.GLTF),
                DeserializeLocationBoundsExtensions(gltfData.GLTF),
                DeserializeCollidersExtensions(gltfData.GLTF),
                DeserializeJointsExtensions(gltfData.GLTF),
                DeserializeRigidbodyExtensions(gltfData.GLTF),
                DeserializeSubItemExtensions(gltfData.GLTF),
                DeserializePlayerSpawnPointExtensions(gltfData.GLTF),
                DeserializePlayerSpawnPointRestrictionExtensions(gltfData.GLTF),
                DeserializeTextExtensions(gltfData.GLTF),
                DeserializeRectTransformExtensions(gltfData.GLTF),
                DeserializeEffekseerEmittersExtensions(gltfData.GLTF)
            );
        }

        public static VciMigrationFlags CheckMigrationFlags(GltfData data)
        {
            if (data.GLTF.extensions.TryDeserializeExtensions(glTF_VCAST_vci_meta.ExtensionName,
                glTF_VCAST_vci_meta_Deserializer.Deserialize, out var extMeta))
            {
                var vciFlags = new VciMigrationFlags(extMeta);
                // UniGLTF 向けの Migration Flag 設定
                // UniVCI v0.33 未満のバージョンの場合は、PBR の smoothness texture の値が 2 乗されているため、インポート時に手心を加える
                data.MigrationFlags.IsRoughnessTextureValueSquared = vciFlags.ExportedVciVersionNumber < 33;

                return vciFlags;
            }
            return new VciMigrationFlags(null);
        }

        public static glTF_VCAST_vci_embedded_script DeserializeScriptExtension(glTF gltf)
        {
            if (gltf?.extensions == null)
            {
                return null;
            }
            if (!gltf.extensions.TryDeserializeExtensions(glTF_VCAST_vci_embedded_script.ExtensionName,
                glTF_VCAST_vci_embedded_script_Deserializer.Deserialize, out var extension))
            {
                return null;
            }

            // NOTE: Windows FileSystem は大文字小文字の違いは同名ファイルとして扱ってしまうため, IgnoreCase で評価する.
            var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (var scriptIdx = 0; scriptIdx < extension.scripts.Count; ++scriptIdx)
            {
                var x = extension.scripts[scriptIdx];

                // 名前の空白または重複の場合は代わりの名前を付ける
                if (string.IsNullOrEmpty(x.name))
                {
                    if (scriptIdx == 0)
                    {
                        x.name = "main";
                    }
                    else
                    {
                        x.name = $"script_{scriptIdx}";
                    }
                }
                x.name = GlbLowLevelParser.FixNameUnique(used, x.name);
            }

            return extension;
        }

        public static glTF_VCAST_vci_audios DeserializeAudioExtension(glTF gltf)
        {
            if (gltf?.extensions == null)
            {
                return null;
            }
            if (!gltf.extensions.TryDeserializeExtensions(glTF_VCAST_vci_audios.ExtensionName,
                glTF_VCAST_vci_audios_Deserializer.Deserialize, out var audioExt))
            {
                return null;
            }

            // NOTE: Windows FileSystem は大文字小文字の違いは同名ファイルとして扱ってしまうため, IgnoreCase で評価する.
            var used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (var audioIdx = 0; audioIdx < audioExt.audios.Count; ++audioIdx)
            {
                var x = audioExt.audios[audioIdx];

                // 名前の空白または重複の場合は代わりの名前を付ける
                if (string.IsNullOrEmpty(x.name))
                {
                    x.name = $"audio_{audioIdx}";
                }
                x.name = GlbLowLevelParser.FixNameUnique(used, x.name);
            }

            return audioExt;
        }

        public static glTF_VCAST_vci_material_unity DeserializeUnityMaterialExtension(glTF gltf)
        {
            if (gltf.extensions.TryDeserializeExtensions(glTF_VCAST_vci_material_unity.ExtensionName,
                glTF_VCAST_vci_material_unity_Deserializer.Deserialize,
                out var extMaterial))
            {
                return extMaterial;
            }

            Debug.LogWarning($"This file has no {nameof(glTF_VCAST_vci_material_unity)} extension.");
            extMaterial = new glTF_VCAST_vci_material_unity
            {
                materials = gltf.materials.Select(x => new VciMaterialJsonObject()).ToList()
            };

            return extMaterial;
        }

        public static glTF_VCAST_vci_location_lighting DeserializeLocationLightingExtension(glTF gltf)
        {
            if (gltf?.extensions == null) return null;
            if (gltf.extensions.TryDeserializeExtensions(glTF_VCAST_vci_location_lighting.ExtensionName,
                glTF_VCAST_vci_location_lighting_Deserializer.Deserialize,
                out var extension))
            {
                return extension;
            }
            else
            {
                return null;
            }
        }

        public static glTF_VCAST_vci_spring_bone DeserializeSpringBoneExtension(glTF gltf)
        {
            if (gltf?.extensions == null) return null;
            if (gltf.extensions.TryDeserializeExtensions(
                glTF_VCAST_vci_spring_bone.ExtensionName,
                glTF_VCAST_vci_spring_bone_Deserializer.Deserialize,
                out var extension))
            {
                return extension;
            }
            else
            {
                return null;
            }
        }

        public static glTF_Effekseer DeserializeEffekseerExtension(glTF gltf)
        {
            if (gltf?.extensions == null) return null;
            if (gltf.extensions.TryDeserializeExtensions(
                glTF_Effekseer.ExtensionName,
                glTF_Effekseer_Deserializer.Deserialize,
                out var extension))
            {
                return extension;
            }
            else
            {
                return null;
            }
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_audio_sources ext)> DeserializeAudioSourcesNodeExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_audio_sources.ExtensionName,
                glTF_VCAST_vci_audio_sources_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_animation ext)> DeserializeAnimationNodeExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_animation.ExtensionName,
                glTF_VCAST_vci_animation_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_attachable extension)> DeserializeAttachableExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_attachable.ExtensionName,
                glTF_VCAST_vci_attachable_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_lightmap extension)> DeserializeLightmapExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_lightmap.ExtensionName,
                glTF_VCAST_vci_lightmap_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_reflectionProbe extension)> DeserializeReflectionProbeExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_reflectionProbe.ExtensionName,
                glTF_VCAST_vci_reflectionProbe_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_location_bounds extension)> DeserializeLocationBoundsExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_location_bounds.ExtensionName,
                glTF_VCAST_vci_location_bounds_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> DeserializeCollidersExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_colliders.ExtensionName,
                glTF_VCAST_vci_colliders_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_joints extension)> DeserializeJointsExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_joints.ExtensionName,
                glTF_VCAST_vci_joints_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> DeserializeRigidbodyExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_rigidbody.ExtensionName,
                glTF_VCAST_vci_rigidbody_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> DeserializeSubItemExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_item.ExtensionName,
                glTF_VCAST_vci_item_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point extension)> DeserializePlayerSpawnPointExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_player_spawn_point.ExtensionName,
                glTF_VCAST_vci_player_spawn_point_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point_restriction extension)> DeserializePlayerSpawnPointRestrictionExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_player_spawn_point_restriction.ExtensionName,
                glTF_VCAST_vci_player_spawn_point_restriction_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_text extension)> DeserializeTextExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_text.ExtensionName,
                glTF_VCAST_vci_text_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_VCAST_vci_rectTransform extension)> DeserializeRectTransformExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_VCAST_vci_rectTransform.ExtensionName,
                glTF_VCAST_vci_rectTransform_Deserializer.Deserialize
            );
        }

        public static List<(int gltfNodeIdx, glTF_Effekseer_emitters extension)> DeserializeEffekseerEmittersExtensions(glTF gltf)
        {
            return DeserializeNodeExtensions(
                gltf,
                glTF_Effekseer_emitters.ExtensionName,
                glTF_Effekseer_emitters_Deserializer.Deserialize
            );
        }

        private static List<(int gltfNodeIdx, T extension)> DeserializeNodeExtensions<T>(glTF gltf, string extensionName, Func<JsonNode, T> deserializer)
        {
            var extensions = new List<(int, T)>();
            for (var nodeIdx = 0; nodeIdx < gltf.nodes.Count; nodeIdx++)
            {
                var node = gltf.nodes[nodeIdx];
                if (node.extensions.TryDeserializeExtensions(extensionName, deserializer, out var extension))
                {
                    extensions.Add((nodeIdx, extension));
                }
            }
            return extensions;
        }
    }
}
