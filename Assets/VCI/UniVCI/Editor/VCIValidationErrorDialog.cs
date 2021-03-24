using System;
using UnityEditor;

namespace VCI
{
    public class VCIValidationErrorDialog
    {
        public static void ShowErrorDialog(VCIValidatorException e)
        {
            var title = $"Error{(int) e.ErrorType}";
            var text = e.Message;
            text = text.Replace("\\n", Environment.NewLine);

            if (e.ErrorType == ValidationErrorType.InvalidCharacter)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Warning);
            }

            EditorUtility.DisplayDialog(title, text, "OK");
        }
    }
}