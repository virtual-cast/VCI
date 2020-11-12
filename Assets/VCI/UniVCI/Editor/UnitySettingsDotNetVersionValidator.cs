using UnityEditor;

namespace VCI
{
    public sealed class UnitySettingsDotNetVersionValidator : IUnitySettingsValidator
    {
        public bool IsValid => CurrentLevel == ApiCompatibilityLevel.NET_4_6;
        public string ValidationDescription => $"API Compatibility Level (current = {CurrentLevel})";
        public string ValidationButtonText => "Use recommended .NET 4.x";
        public void OnValidate()
        {
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Standalone, ApiCompatibilityLevel.NET_4_6);
        }

        private ApiCompatibilityLevel CurrentLevel => PlayerSettings.GetApiCompatibilityLevel(BuildTargetGroup.Standalone);
    }
}