using Effekseer;
using UnityEngine;

namespace VCI
{
    internal static class VciEffekseerValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateEffekseer(gameObject);
        }

        private static void ValidateEffekseer(GameObject gameObject)
        {
            foreach (var effekseerEmitter in gameObject.GetComponentsInChildren<EffekseerEmitter>())
            {
                if (effekseerEmitter.effectAsset.materialResources.Length == 0) continue;
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.EffekseerMaterialIncluded}");
                throw new VciValidatorException(VciValidationErrorType.EffekseerMaterialIncluded, effekseerEmitter, errorText);
            }
        }
    }
}
