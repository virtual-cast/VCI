using System.IO;
using System.Linq;
using UnityEngine;

namespace VCI
{
    internal static class VciScriptValidator
    {
        public static void Validate(GameObject gameObject)
        {
            var vciObject = gameObject.GetComponent<VCIObject>();
            ValidateScripts(vciObject);
        }

        private static void ValidateScripts(VCIObject vciObject)
        {
            if (!vciObject.Scripts.Any())
            {
                return;
            }

            // Check 1: 一つ目のスクリプトの名前が「main」である
            if (vciObject.Scripts[0].name != "main")
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.FirstScriptNameNotValid}");
                throw new VciValidatorException(VciValidationErrorType.FirstScriptNameNotValid, errorText);
            }

            // Check 2: 名前が空のスクリプトが存在しない
            var empties = vciObject.Scripts.Where(x => string.IsNullOrEmpty(x.name));
            if (empties.Any())
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.NoScriptName}");
                throw new VciValidatorException(VciValidationErrorType.NoScriptName, errorText);
            }

            // Check 3: 同一の名前のスクリプトが複数存在しない
            var duplicates = vciObject.Scripts.GroupBy(script => script.name)
                .Where(name => name.Count() > 1)
                .Select(group => @group.Key).ToList();
            if (duplicates.Any())
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.ScriptNameConfliction}");
                throw new VciValidatorException(VciValidationErrorType.ScriptNameConfliction, errorText);
            }

            // Check 4: スクリプト名に無効な文字列が含まれていない
            // - 無効な文字列 : ファイル名に含めることのできない文字 + '.'
            var invalidChars = Path.GetInvalidFileNameChars().Concat(new[] { '.' }).ToArray();
            foreach (var script in vciObject.Scripts)
            {
                if (script.name.IndexOfAny(invalidChars) >= 0)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.InvalidCharacter}", script.name);
                    throw new VciValidatorException(VciValidationErrorType.InvalidCharacter, errorText);
                }
            }
        }

    }
}
