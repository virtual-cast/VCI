using System.Collections.Generic;

namespace VCI
{
    public interface IUnitySettingsValidator
    {
        bool IsValid { get; }
        string ValidationDescription { get; }
        string ValidationButtonText { get; }
        void OnValidate();
    }

    public static class UnitySettingsValidator
    {
        public static IEnumerable<IUnitySettingsValidator> Validators => _validators;

        private static readonly IUnitySettingsValidator[] _validators =
        {
            new UnitySettingsColorSpaceValidator(),
            new UnitySettingsLayerValidator()
        };

        public static bool IsValidAll()
        {
            foreach (var validator in Validators)
            {
                if (!validator.IsValid) return false;
            }

            return true;
        }
    }
}