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

        private static readonly Dictionary<string, ValidationRule> ValidationRules =
            new Dictionary<string, ValidationRule>
            {
                {"Version", new ValidationRule(false, VCIValidator.VersionTextLength)},
                {"Author", new ValidationRule(true, VCIValidator.AuthorTextLength)},
                {"ContactInformation", new ValidationRule(false, VCIValidator.ContactInformationTextLength)},
                {"Reference", new ValidationRule(false, VCIValidator.ReferenceTextLength)},
                {"Title", new ValidationRule(true, VCIValidator.TitleTextLength)},
                {"Description", new ValidationRule(false, VCIValidator.DescriptionTextLength)},
                {"ModelDataOtherLicenseUrl", new ValidationRule(false, VCIValidator.ModelDataOtherLicenseUrlLength)},
                {"ScriptOtherLicenseUrl", new ValidationRule(false, VCIValidator.ScriptOtherLicenseUrlLength)}
            };

        private static string ValidateField(string fieldName, string text)
        {
            var validationRule = ValidationRules[fieldName];
            if (validationRule.IsRequired && string.IsNullOrEmpty(text))
                return string.Format(VCIConfig.GetText("input"), fieldName);

            if (text != null && validationRule.MaxLength < text.Length)
                return string.Format(VCIConfig.GetText("input_less_than"), fieldName, validationRule.MaxLength);

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