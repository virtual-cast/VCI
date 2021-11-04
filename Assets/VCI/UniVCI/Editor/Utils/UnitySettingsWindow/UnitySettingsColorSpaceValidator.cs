using UnityEditor;
using UnityEngine;

namespace VCI
{
    public sealed class UnitySettingsColorSpaceValidator : IUnitySettingsValidator
    {
        public bool IsValid => PlayerSettings.colorSpace == ColorSpace.Linear;
        public string ValidationDescription => $"Color Space (current = {PlayerSettings.colorSpace})";
        public string ValidationButtonText => "Use recommended Linear";
        public void OnValidate()
        {
            PlayerSettings.colorSpace = ColorSpace.Linear;
        }
    }
}