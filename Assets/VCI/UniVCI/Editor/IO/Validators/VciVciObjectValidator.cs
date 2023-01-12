using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    internal static class VciVciObjectValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateVciObjectComponentRestrictions(gameObject);
        }

        private static void ValidateVciObjectComponentRestrictions(GameObject gameObject)
        {
            // Check 1: RootのGameObjectにVCIObjectがアタッチされている
            var vciObject = gameObject.GetComponent<VCIObject>();
            if (vciObject == null)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.VCIObjectNotAttached}");
                throw new VciValidatorException(VciValidationErrorType.VCIObjectNotAttached, errorText);
            }

            // Check 2:「VCIObject」コンポーネントがVCIの中で一つのみ存在する
            var vciObjectCount = 0;
            var transforms = vciObject.transform.Traverse().ToArray();

            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCIObject>() == null) { continue; }

                vciObjectCount++;
                if (vciObjectCount > 1)
                {
                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.MultipleVCIObject}");
                    throw new VciValidatorException(VciValidationErrorType.MultipleVCIObject, errorText);
                }
            }

            // Check 3: SubItemはVCIObjectと同じGameObjectにアタッチできない・ネストできない
            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCISubItem>() == null) { continue; }
                if (transform.GetComponent<VCIObject>() != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.RootSubItem}", transform.name);
                    throw new VciValidatorException(VciValidationErrorType.RootSubItem, errorText);
                }
                if (transform.parent.parent != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.NestedSubItem}", transform.name);
                    throw new VciValidatorException(VciValidationErrorType.NestedSubItem, errorText);
                }
            }

            // Check 4: AudioSourceはVCIObjectと同じGameObjectにアタッチできない
            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCIObject>() == null) { continue; }
                if (transform.GetComponent<AudioSource>() != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.RootAudioSource}", transform.name);
                    throw new VciValidatorException(VciValidationErrorType.RootAudioSource, errorText);
                }
            }

            // Check 5: VCIObject以下に最低1つはGameObjectがある
            if (vciObject.transform.childCount == 0)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.OnlyVciObject}");
                throw new VciValidatorException(VciValidationErrorType.OnlyVciObject, errorText);
            }
        }

    }
}
