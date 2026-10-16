using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace VCI
{
    /// <summary>
    /// ルートに VCIBakedLighting があるとき、VCI を読み込んだ側で Unity 上と見た目が変わるライティング設定を検出する
    /// </summary>
    internal static class VciBakedLightingValidator
    {
        private static readonly HashSet<string> MToonShaderNames = new HashSet<string>
        {
            "VRM/MToon",
            "VRM10/MToon10",
            "VRM10/Universal Render Pipeline/MToon10",
        };

        public static void Validate(GameObject gameObject)
        {
            if (gameObject.GetComponent<VCIBakedLighting>() == null)
            {
                return;
            }

            if (LightmapSettings.lightmaps.Length == 0)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.LightmapNotBaked}");
                throw new VciValidatorException(VciValidationErrorType.LightmapNotBaked, errorText);
            }

            // Light は VCI に出力されないため、Baked 以外のライトの光は読み込んだ側で失われる
            var nonBakedLight = Object.FindObjectsByType<Light>(FindObjectsSortMode.None)
                .FirstOrDefault(x => x.enabled && x.lightmapBakeType != LightmapBakeType.Baked);
            if (nonBakedLight != null)
            {
                var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.NonBakedLightExists}", nonBakedLight.name);
                throw new VciValidatorException(VciValidationErrorType.NonBakedLightExists, nonBakedLight, errorText);
            }

            // 出力時に方向テクスチャは捨てられ、Non-Directional として扱われる
            if (LightmapSettings.lightmapsMode != LightmapsMode.NonDirectional)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.DirectionalLightmapNotSupported}");
                throw new VciValidatorException(VciValidationErrorType.DirectionalLightmapNotSupported, errorText);
            }

            var lightProbes = LightmapSettings.lightProbes;
            if (lightProbes == null || lightProbes.count == 0)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.LightProbeNotFound}");
                throw new VciValidatorException(VciValidationErrorType.LightProbeNotFound, errorText);
            }

            // Fog は VCI に出力されない
            if (RenderSettings.fog)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.FogEnabled}");
                throw new VciValidatorException(VciValidationErrorType.FogEnabled, errorText);
            }

            // Baked 以外のリフレクションプローブは出力時に捨てられる
            var nonBakedReflectionProbe = gameObject.GetComponentsInChildren<ReflectionProbe>()
                .FirstOrDefault(x => x.enabled && x.mode != ReflectionProbeMode.Baked);
            if (nonBakedReflectionProbe != null)
            {
                var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.NonBakedReflectionProbeExists}", nonBakedReflectionProbe.name);
                throw new VciValidatorException(VciValidationErrorType.NonBakedReflectionProbeExists, nonBakedReflectionProbe, errorText);
            }

            // MToon はライトマップを読まず SH だけで照らされる。ライトマップを受けるとライトプローブも効かず、読み込んだ側で真っ黒になる
            var mToonReceivingLightmap = gameObject.GetComponentsInChildren<MeshRenderer>()
                .FirstOrDefault(x =>
                    GameObjectUtility.GetStaticEditorFlags(x.gameObject).HasFlag(StaticEditorFlags.ContributeGI)
                    && x.receiveGI == ReceiveGI.Lightmaps
                    && x.sharedMaterials.Any(m => m != null && MToonShaderNames.Contains(m.shader.name)));
            if (mToonReceivingLightmap != null)
            {
                var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.MToonReceivesLightmap}", mToonReceivingLightmap.name);
                throw new VciValidatorException(VciValidationErrorType.MToonReceivesLightmap, mToonReceivingLightmap, errorText);
            }
        }
    }
}
