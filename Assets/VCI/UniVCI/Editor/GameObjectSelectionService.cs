using UnityEditor;
using UnityEngine;

namespace VCI
{
    public sealed class GameObjectSelectionService
    {
        // TODO: VCIのValidationに関するエラーではないので、別途エラーを定義して投げる
        // NOTE: VCIExporterMenu.Validateの処理の一部をここに切り出したため、VCIValidatorExceptionを投げる形になっている
        //       切り出したのは、この処理は厳密にはVCIのValidationに関するものではないと思ったため
        /// <summary>
        /// Editor上で選択されている唯一のGameObjectを返す
        /// GameObjectが選択されていない/複数選択されている場合はexceptionをthrowする
        /// </summary>
        /// <returns></returns>
        /// <exception cref="VCIValidatorException"></exception>
        public static GameObject GetSingleSelectedObject()
        {
            var selectedGameObjects = Selection.gameObjects;
            if (selectedGameObjects.Length == 0)
            {
                var errorText = VCIConfig.GetText($"error{(int) ValidationErrorType.GameObjectNotSelected}");
                throw new VCIValidatorException(ValidationErrorType.GameObjectNotSelected, errorText);
            }

            if (2 <= selectedGameObjects.Length)
            {
                var errorText = VCIConfig.GetText($"error{(int) ValidationErrorType.MultipleSelection}");
                throw new VCIValidatorException(ValidationErrorType.MultipleSelection, errorText);
            }

            return selectedGameObjects[0];
        }
    }
}