using System;
using System.Collections.Generic;
using UniGLTF;
using UniJSON;

namespace VCI
{
    /// <summary>
    /// UniVCI で使用する glTF 拡張の一覧を持ち、シリアライザの AOT 用事前コード生成を容易にするために存在するクラス.
    /// このクラスに対応する glTF 拡張は存在しない.
    /// </summary>
    [Serializable]
    public sealed class VciAllExtensions
    {
        // vci base extensions
        public glTF_VCAST_vci_meta VCAST_vci_meta;
        public glTF_VCAST_vci_item VCAST_vci_item;

        // Animation
        public glTF_VCAST_vci_animation VCAST_vci_animation;

        // Attachable
        public glTF_VCAST_vci_attachable VCAST_vci_attachable;

        // Material
        public glTF_VCAST_vci_material_unity VCAST_vci_material_unity;
        public glTF_VCAST_materials_pbr VCAST_materials_pbr;

        // Physics
        public glTF_VCAST_vci_colliders VCAST_vci_collider;
        public glTF_VCAST_vci_rigidbody VCAST_vci_rigidbody;
        public glTF_VCAST_vci_joints VCAST_vci_joints;
        public glTF_VCAST_vci_spring_bone VCAST_vci_spring_bone;

        // Audio
        public glTF_VCAST_vci_audios VCAST_vci_audios;
        public glTF_VCAST_vci_audio_sources VCAST_vci_audio_sources;

        // Script
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
    }
}


