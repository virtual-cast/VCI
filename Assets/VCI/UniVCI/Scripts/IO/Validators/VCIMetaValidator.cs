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

        static readonly int VersionTextLength = 30;
        static readonly int AuthorTextLength = 30;
        static readonly int ContactInformationTextLength = 255;
        static readonly int ReferenceTextLength = 255;
        static readonly int TitleTextLength = 30;
        static readonly int DescriptionTextLength = 500;
        static readonly int ModelDataOtherLicenseUrlLength = 2048;
        static readonly int ScriptOtherLicenseUrlLength = 2048;

        private static readonly Dictionary<string, ValidationRule> ValidationRules =
            new Dictionary<string, ValidationRule>
            {
                {"Version", new ValidationRule(false, VersionTextLength)},
                {"Author", new ValidationRule(true, AuthorTextLength)},
                {"ContactInformation", new ValidationRule(false, ContactInformationTextLength)},
                {"Reference", new ValidationRule(false, ReferenceTextLength)},
                {"Title", new ValidationRule(true, TitleTextLength)},
                {"Description", new ValidationRule(false, DescriptionTextLength)},
                {"ModelDataOtherLicenseUrl", new ValidationRule(false, ModelDataOtherLicenseUrlLength)},
                {"ScriptOtherLicenseUrl", new ValidationRule(false, ScriptOtherLicenseUrlLength)}
            };

        private static string ValidateField(string fieldName, string text)
        {
            var validationRule = ValidationRules[fieldName];
            if (validationRule.IsRequired && string.IsNullOrEmpty(text))
                return VCIConfig.GetFormattedText("input", fieldName);

            if (text != null && validationRule.MaxLength < text.Length)
                return VCIConfig.GetFormattedText("input_less_than", fieldName, validationRule.MaxLength);

            return "";
        }

        public static void Validate(VCIObject vciObject)
        {
            var errorMessages = new[]
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
                throw new VCIValidatorException(
                    ValidationErrorType.InvalidMetaData,
                    string.Join(Environment.NewLine, errorMessages.Where(m => m != "").ToArray()));
            }
        }
    }
}