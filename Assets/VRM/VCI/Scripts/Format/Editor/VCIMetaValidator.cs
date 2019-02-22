using System;
using System.Collections.Generic;
using System.Linq;

namespace VCI
{
    public static class VCIMetaValidator
    {
        private struct ValidationRule
        {
            public readonly bool IsRequired;
            public readonly int MaxLength;

            public ValidationRule(bool isRequired, int maxLength)
            {
                IsRequired = isRequired;
                MaxLength = maxLength;
            }
        }

        private static readonly Dictionary<string, ValidationRule> ValidationRules = new Dictionary<string, ValidationRule>
        {
            { "Version", new ValidationRule(false, 255) },
            { "Author", new ValidationRule(true, 255) },
            { "ContactInformation", new ValidationRule(false, 255) },
            { "Reference", new ValidationRule(false, 255) },
            { "Title", new ValidationRule(true, 255) },
            { "Description", new ValidationRule(false, 500) },
            { "ModelDataOtherLicenseUrl", new ValidationRule(false, 2048) },
            { "ScriptOtherLicenseUrl", new ValidationRule(false, 2048 )}
        };

        private static string ValidateField(string fieldName, string text)
        {
            var validationRule = ValidationRules[fieldName];
            if (validationRule.IsRequired && string.IsNullOrEmpty(text))
            {
                return $"{fieldName}を入力してください。";
            }

            if (text != null && validationRule.MaxLength < text.Length)
            {
                return $"{fieldName}を{validationRule.MaxLength}文字以内にして下さい。";
            }

            return "";
        }

        public static bool Validate(VCIObject vciObject, out string errorMessage)
        {
            var errorMessages = new []
            {
                ValidateField("Version", vciObject.Meta.version),
                ValidateField("Author", vciObject.Meta.author),
                ValidateField("ContactInformation", vciObject.Meta.contactInformation),
                ValidateField("Reference", vciObject.Meta.reference),
                ValidateField("Title", vciObject.Meta.title),
                ValidateField("Description", vciObject.Meta.description),
                ValidateField("ModelDataOtherLicenseUrl", vciObject.Meta.modelDataOtherLicenseUrl),
                ValidateField("ScriptOtherLicenseUrl", vciObject.Meta.scriptOtherLicenseUrl)
            };

            if (errorMessages.Any(m => m != ""))
            {
                errorMessage = string.Join(Environment.NewLine, errorMessages.Where(m => m != ""));
                return false;
            }

            errorMessage = "";
            return true;
        }
    }
}