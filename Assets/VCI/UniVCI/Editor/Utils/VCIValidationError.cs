using System;
using UnityEditor;

namespace VCI
{
    public sealed class VCIValidationError
    {
        public static void ShowErrorDialog(VCIValidatorException e)
        {
            var title = $"Error{(int)e.ErrorType}";
            var text = e.Message;
            text = text.Replace("\\n", Environment.NewLine);

            if (e.ErrorType == ValidationErrorType.InvalidCharacter)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Warning);
            }

            EditorUtility.DisplayDialog(title, text, "OK");
        }

        /// <summary>
        /// Exception に対象となるオブジェクトが設定されている場合は、シーン上で選択をする
        /// </summary>
        public static void SelectObject(VCIValidatorException e)
        {
            if (e.Object != null)
            {
                Selection.SetActiveObjectWithContext(e.Object, null);
            }
        }
    }
}
