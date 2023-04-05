using UnityEngine;

namespace VCI
{
    public static class VciValidator
    {
        public static void ValidateVciRequirements(GameObject gameObject)
        {
            VciVciObjectValidator.Validate(gameObject);
            VciSubItemValidator.Validate(gameObject);
            VciMetaValidator.Validate(gameObject);
            VciScriptValidator.Validate(gameObject);

            VciPhysicsValidator.Validate(gameObject);
            VciRendererValidator.Validate(gameObject);
            VciAnimationValidator.Validate(gameObject);
            VciAudioValidator.Validate(gameObject);
            VciSpringBoneValidator.Validate(gameObject);
            VciEffekseerValidator.Validate(gameObject);

            VciLocationValidator.Validate(gameObject);
        }
    }
}
