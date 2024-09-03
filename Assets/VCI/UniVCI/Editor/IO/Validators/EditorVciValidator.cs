using UnityEngine;

namespace VCI
{
    public static class EditorVciValidator
    {
        public static void ValidateVciRequirements(GameObject gameObject)
        {
            RuntimeVciValidator.ValidateVciRequirements(gameObject);

            VciVciObjectValidator.Validate(gameObject);
            VciSubItemValidator.Validate(gameObject);
            VciMetaValidator.Validate(gameObject);
            VciScriptValidator.Validate(gameObject);

            VciRendererValidator.Validate(gameObject);
            VciAnimationValidator.Validate(gameObject);
            VciAudioValidator.Validate(gameObject);
            VciSpringBoneValidator.Validate(gameObject);
            VciEffekseerValidator.Validate(gameObject);

            VciLocationValidator.Validate(gameObject);
        }
    }
}
