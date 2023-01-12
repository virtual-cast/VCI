using System.Linq;
using UnityEngine;

namespace VCI
{
    internal static class VciSubItemValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateSubItemKey(gameObject);
        }

        private static void ValidateSubItemKey(GameObject gameObject)
        {
            var subItems = gameObject.GetComponentsInChildren<VCISubItem>();

            // Check 1: SubItem Key が定義されていない
            var undefinedKeySubItem = subItems.FirstOrDefault(subItem => subItem.Key == 0);
            if (undefinedKeySubItem != null)
            {
                // NOTE: 通常フローでは必ず設定されているはずなので、ユーザ向けメッセージは定義されない
                throw new VciValidatorException(VciValidationErrorType.SubItemKeyUndefined, undefinedKeySubItem, $"undefined SubItem Key");
            }

            // Check 2: SubItem Key は重複しない
            var duplicateKeysGroup = subItems
                .GroupBy(subItem => subItem.Key)
                .FirstOrDefault(group => group.Count() > 1);

            if (duplicateKeysGroup != null)
            {
                // NOTE: 通常フローでは重複し得ないので、ユーザ向けメッセージは定義されない
                var obj = duplicateKeysGroup.First();
                throw new VciValidatorException(VciValidationErrorType.SubItemKeyDuplicated, obj, $"duplicated SubItem Key: {duplicateKeysGroup.Key}");
            }
        }
    }
}
