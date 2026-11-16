using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    public sealed class UnitySettingsLodBiasValidator : IUnitySettingsValidator
    {
        private static readonly float RecommendedLodBias = 1f;
        private static SerializedObject _qualitySettings;

        public bool IsValid => IsAllQualityLevelsValid();
        public string ValidationDescription => $"LOD Group Bias ({QualitySettings.lodBias} for quality level {QualitySettings.GetQualityLevel()})";
        public string ValidationButtonText => $"Use recommended LOD Bias {RecommendedLodBias} for all quality levels";

        public void OnValidate()
        {
            var qualitySettings = LoadQualitySettings();
            foreach (var level in EnumerateLevels(qualitySettings))
            {
                level.floatValue = RecommendedLodBias;
            }
            qualitySettings.ApplyModifiedProperties();
        }

        private static bool IsAllQualityLevelsValid()
            => EnumerateLevels(LoadQualitySettings())
                .All(level => Mathf.Approximately(level.floatValue, RecommendedLodBias));

        private static IEnumerable<SerializedProperty> EnumerateLevels(SerializedObject qualitySettings)
        {
            var levels = qualitySettings.FindProperty("m_QualitySettings");
            for (var i = 0; i < levels.arraySize; i++)
            {
                yield return levels.GetArrayElementAtIndex(i).FindPropertyRelative("lodBias");
            }
        }

        private static SerializedObject LoadQualitySettings()
        {
            if (_qualitySettings == null || _qualitySettings.targetObject == null)
            {
                _qualitySettings?.Dispose();
                _qualitySettings = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/QualitySettings.asset")[0]);
            }
            _qualitySettings.Update();
            return _qualitySettings;
        }
    }
}
