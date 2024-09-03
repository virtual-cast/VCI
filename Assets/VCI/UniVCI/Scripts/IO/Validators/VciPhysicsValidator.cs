using UnityEngine;

namespace VCI
{
    internal static class VciPhysicsValidator
    {
        public static void Validate(GameObject rootGameObject)
        {
            ValidateRigidbody(rootGameObject);
            ValidateMeshCollider(rootGameObject);
        }

        private static void ValidateRigidbody(GameObject rootGameObject)
        {
            // Check 1: SubItemにRigidbodyがアタッチされている
            var subItems = rootGameObject.GetComponentsInChildren<VCISubItem>();
            foreach (var subItem in subItems)
            {
                if (subItem.GetComponent<Rigidbody>() == null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.SubItemWithoutRigidbody}", subItem.gameObject.name);
                    throw new VciValidatorException(VciValidationErrorType.SubItemWithoutRigidbody, subItem, errorText);
                }
            }
        }

        private static void ValidateMeshCollider(GameObject rootGameObject)
        {
            // Check 1: Convex でない MeshCollider が Rigidbody の子孫にある場合は、これを弾く。
            //          Convex でない MeshCollider は Rigidbody 以下では意味をなさないため。
            //          またその場合、間違った理解のもと使っている可能性が高い。
            foreach (var meshCollider in rootGameObject.GetComponentsInChildren<MeshCollider>())
            {
                if (!meshCollider.convex)
                {
                    if (meshCollider.gameObject.GetComponentInParent<Rigidbody>() != null)
                    {
                        throw new VciValidatorException(
                            VciValidationErrorType.NonConvexMeshColliderIsUnderRigidbody,
                            meshCollider,
                            $"The non-convex MeshCollider is under the rigidbody: {meshCollider.gameObject.name}"
                        );
                    }
                }
            }
        }
    }
}
