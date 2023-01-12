using UnityEngine;

namespace VCI
{
    internal static class VciRendererValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateRenderer(gameObject);
        }

        private static void ValidateRenderer(GameObject gameObject)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            if (renderers == null || renderers.Length == 0) return;

            // Check 1: MeshRenderer, SkinnedMeshRenderer以外のRendererが含まれていない
            foreach (var renderer in renderers)
            {
                switch (renderer)
                {
                    case MeshRenderer _:
                    case SkinnedMeshRenderer _:
                        break;
                    default:
                        var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.InvalidComponent}", renderer.GetType().Name);
                        throw new VciValidatorException(VciValidationErrorType.RootIsAnimated, renderer, errorText);
                }
            }
        }
    }
}
