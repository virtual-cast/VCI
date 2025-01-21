using System.Collections.Generic;
using UniGLTF;

namespace VCI
{
    public static class VciMigrator
    {
        /// <summary>
        /// 現行のVCI仕様に合致しない部分を修正して、<see cref="VciData"/>を生成する。
        /// 副作用がある（渡された値が変更される）ため注意。
        /// 現状、呼び出し元でこのメソッドに渡される値はVciDataに格納する以外の用途がないため問題ない。
        /// </summary>
        public static VciData Migrate(
            GltfData gltfData,
            VciMigrationFlags vciMigrationFlags, // 将来的に消したい
            glTF_VCAST_vci_meta meta,
            glTF_VCAST_vci_embedded_script script,
            glTF_VCAST_vci_audios audios,
            glTF_VCAST_vci_material_unity unityMaterials,
            glTF_VCAST_vci_location_lighting locationLighting,
            glTF_VCAST_vci_location_bounds locationBounds,
            glTF_VCAST_vci_spring_bone springBone,
            glTF_Effekseer effekseer,
            List<(int gltfNodeIdx, glTF_VCAST_vci_audio_sources ext)> audioSourcesNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_animation ext)> animationNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_attachable extension)> attachableNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_lightmap extension)> lightmapNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_reflectionProbe extension)> reflectionProbeNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_colliders extension)> collidersNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_joints extension)> jointsNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_rigidbody extension)> rigidbodyNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_item extension)> subItemNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point extension)> playerSpawnPointNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_player_spawn_point_restriction extension)> playerSpawnPointRestrictionNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_text extension)> textNodes,
            List<(int gltfNodeIdx, glTF_VCAST_vci_rectTransform extension)> rectTransformNodes,
            List<(int gltfNodeIdx, glTF_Effekseer_emitters extension)> effekseerEmittersNodes)
        {
            VciPhysicsMigrator.Migrate(gltfData, collidersNodes, rigidbodyNodes, subItemNodes);

            return new VciData(
                gltfData,
                vciMigrationFlags,
                meta,
                script,
                audios,
                unityMaterials,
                locationLighting,
                locationBounds,
                springBone,
                effekseer,
                audioSourcesNodes,
                animationNodes,
                attachableNodes,
                lightmapNodes,
                reflectionProbeNodes,
                collidersNodes,
                jointsNodes,
                rigidbodyNodes,
                subItemNodes,
                playerSpawnPointNodes,
                playerSpawnPointRestrictionNodes,
                textNodes,
                rectTransformNodes,
                effekseerEmittersNodes
            );
        }
    }
}
