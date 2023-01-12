using UnityEngine;

namespace VCI
{
    internal static class VciValidatorUtils
    {
        /// <summary>
        /// 指定の型の Component が Root 以下に存在したら、例外を投げる
        /// </summary>
        public static void ThrowIfExistsTheComponentInChildren<T>(this GameObject rootGameObject) where T : Component
        {
            var c = rootGameObject.GetComponentInChildren<T>(true);
            if (c == null) { return; }

            var gameObjectName = c.name;
            var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.InvalidComponent}", gameObjectName, typeof(T).Name);

            throw new VciValidatorException(VciValidationErrorType.InvalidComponent, c, errorText);
        }
    }
}
