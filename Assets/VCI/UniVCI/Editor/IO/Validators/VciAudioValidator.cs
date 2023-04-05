using UnityEngine;

namespace VCI
{
    internal static class VciAudioValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateAudioSources(gameObject);
        }

        // NOTE: 引数のgameObjectは、VCIのrootのgameObject（VCIObjectコンポーネントがアタッチされているgameObject）である
        private static void ValidateAudioSources(GameObject gameObject)
        {
            CheckForInvalidAudioRollOffSetting(gameObject);
        }

        private static void CheckForInvalidAudioRollOffSetting(GameObject gameObject)
        {
            var audioSources = gameObject.GetComponentsInChildren<AudioSource>();

            foreach (var audioSource in audioSources)
            {
                // NOTE: RollOffMode は Logarithmic しか出力できないため、ここで確認し、それ以外になっている場合にはユーザーに
                //       出力できない旨を知らせる
                if (audioSource.rolloffMode != AudioRolloffMode.Logarithmic)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.RollOffModeNotSupported}", audioSource.name);
                    throw new VciValidatorException(VciValidationErrorType.RollOffModeNotSupported, audioSource, errorText);
                }
            }
        }
    }
}
