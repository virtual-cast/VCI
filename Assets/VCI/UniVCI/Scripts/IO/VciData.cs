using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    public sealed class VciData
    {
        public GltfData GltfData { get; }
        public VciMigrationFlags VciMigrationFlags { get; }
        public glTF_VCAST_vci_embedded_script Script { get; }
        public glTF_VCAST_vci_audios Audios { get; }
        public glTF_VCAST_vci_material_unity UnityMaterials { get; }
        public glTF_VCAST_vci_location_lighting LocationLighting { get; }
        public glTF_VCAST_vci_spring_bone SpringBone { get; }
        public glTF_Effekseer Effekseer { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_audio_sources ext)> AudioSourcesNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_animation ext)> AnimationNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_attachable extension)> AttachableNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_lightmap extension)> LightmapNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_reflectionProbe extension)> ReflectionProbeNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_location_bounds extension)> LocationBoundsNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> CollidersNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_joints extension)> JointsNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> RigidbodyNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> SubItemNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point extension)> PlayerSpawnPointNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point_restriction extension)> PlayerSpawnPointRestrictionNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_text extension)> TextNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_rectTransform extension)> RectTransformNodes { get; }
        public IReadOnlyList<(int gltfNodeIdx, glTF_Effekseer_emitters extension)> EffekseerEmittersNodes { get; }

        public VciData(
            GltfData gltfData,
            VciMigrationFlags vciMigrationFlags,
            glTF_VCAST_vci_embedded_script script,
            glTF_VCAST_vci_audios audios,
            glTF_VCAST_vci_material_unity unityMaterials,
            glTF_VCAST_vci_location_lighting locationLighting,
            glTF_VCAST_vci_spring_bone springBone,
            glTF_Effekseer effekseer,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_audio_sources ext)> audioSourcesNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_animation ext)> animationNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_attachable extension)> attachableNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_lightmap extension)> lightmapNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_reflectionProbe extension)> reflectionProbeNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_location_bounds extension)> locationBoundsNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> collidersNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_joints extension)> jointsNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> rigidbodyNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> subItemNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point extension)> playerSpawnPointNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point_restriction extension)> playerSpawnPointRestrictionNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_text extension)> textNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_VCAST_vci_rectTransform extension)> rectTransformNodes,
            IReadOnlyList<(int gltfNodeIdx, glTF_Effekseer_emitters extension)> effekseerEmittersNodes)
        {
            GltfData = gltfData;
            VciMigrationFlags = vciMigrationFlags;
            Script = script;
            Audios = audios;
            UnityMaterials = unityMaterials;
            LocationLighting = locationLighting;
            SpringBone = springBone;
            Effekseer = effekseer;
            AudioSourcesNodes = audioSourcesNodes;
            AnimationNodes = animationNodes;
            AttachableNodes = attachableNodes;
            LightmapNodes = lightmapNodes;
            ReflectionProbeNodes = reflectionProbeNodes;
            LocationBoundsNodes = locationBoundsNodes;
            CollidersNodes = collidersNodes;
            JointsNodes = jointsNodes;
            RigidbodyNodes = rigidbodyNodes;
            SubItemNodes = subItemNodes;
            PlayerSpawnPointNodes = playerSpawnPointNodes;
            PlayerSpawnPointRestrictionNodes = playerSpawnPointRestrictionNodes;
            TextNodes = textNodes;
            RectTransformNodes = rectTransformNodes;
            EffekseerEmittersNodes = effekseerEmittersNodes;
        }
    }
}
