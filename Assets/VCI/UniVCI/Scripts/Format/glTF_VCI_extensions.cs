using System;
using System.Collections.Generic;
using UniGLTF;
using UniJSON;

namespace VCI
{
    public static class VciSerializerExtensions
    {
        public static bool TryDeserializeExtensions<T>(this UniGLTF.glTFExtension extension, string extensionName,  Func<JsonNode, T> deserializer, out T vci)
        {
            if (extension is glTFExtensionImport import)
            {
                foreach (var kv in import.ObjectItems())
                {
                    if (kv.Key.GetString() == extensionName)
                    {
                        vci = deserializer(kv.Value);
                        return true;
                    }
                }
            }

            vci = default;
            return false;
        }
    }

    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci", Description = @"
VCI extension is VirtualCast Interaction.
")]
    public sealed class glTF_VCI_extensions
    {
        // vci base extensions
        public glTF_VCAST_vci_meta VCAST_vci_meta;
        public glTF_VCAST_vci_item VCAST_vci_item;
        public glTF_VCAST_vci_animation VCAST_vci_animation;
        public glTF_VCAST_vci_attachable VCAST_vci_attachable;

        // material
        public glTF_VCAST_vci_material_unity VCAST_vci_material_unity;
        public glTF_VCAST_materials_pbr VCAST_materials_pbr;

        // Physics
        public glTF_VCAST_vci_colliders VCAST_vci_collider;
        public glTF_VCAST_vci_rigidbody VCAST_vci_rigidbody;
        public glTF_VCAST_vci_joints VCAST_vci_joints;
        public glTF_VCAST_vci_spring_bone VCAST_vci_spring_bone;

        // audio
        public glTF_VCAST_vci_audios VCAST_vci_audios;
        public glTF_VCAST_vci_audio_sources VCAST_vci_audio_sources;

        // script
        public glTF_VCAST_vci_embedded_script VCAST_vci_embedded_script;

        // TextMeshPro
        public glTF_VCAST_vci_text VCAST_vci_text;
        public glTF_VCAST_vci_rectTransform VCAST_vci_rectTransform;

        // Effekseer
        public glTF_Effekseer Effekseer;
        public glTF_Effekseer_emitters Effekseer_emitters;

        // Lightmap
        public glTF_VCAST_vci_lightmap VCAST_vci_lightmap;
        public glTF_VCAST_vci_location_lighting VCAST_vci_location_lighting;
        public glTF_VCAST_vci_reflectionProbe VCAST_vci_reflectionProbe;

        // Location
        public glTF_VCAST_vci_location_bounds VCAST_vci_location_bounds;
        public glTF_VCAST_vci_player_spawn_point VCAST_vci_player_spawn_point;
        public glTF_VCAST_vci_player_spawn_point_restriction VCAST_vci_player_spawn_point_restriction;

        public static bool TryDeserilize<T>(glTFExtension extension, string extensionName, Func<JsonNode, T> deserializer, out T vci)
        {
            if (extension is glTFExtensionImport import)
            {
                foreach (var kv in import.ObjectItems())
                {
                    if (kv.Key.GetString() == extensionName)
                    {
                        vci = deserializer(kv.Value);
                        return true;
                    }
                }
            }

            vci = default;
            return false;
        }
    }
}


