using System;
using UnityEditor;

namespace VCI
{
    public static class VciValidatorExceptionUtils
    {
        public static void ShowErrorDialog(VciValidatorException e)
        {
            var title = $"Error{(int)e.ErrorType}";
            var text = e.Message;
            text = text.Replace("\\n", Environment.NewLine);

            if (e.ErrorType == VciValidationErrorType.InvalidCharacter)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Warning);
            }

            EditorUtility.DisplayDialog(title, text, "OK");
        }

        /// <summary>
        /// Exception に対象となるオブジェクトが設定されている場合は、シーン上で選択をする
        /// </summary>
        public static void SelectObject(VciValidatorException e)
        {
            if (e.Object != null)
            {
                Selection.SetActiveObjectWithContext(e.Object, null);
            }
        }
    }
}
