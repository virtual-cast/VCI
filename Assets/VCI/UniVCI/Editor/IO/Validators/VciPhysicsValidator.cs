using UnityEngine;

namespace VCI
{
    internal static class VciPhysicsValidator
    {
        public static void Validate(GameObject rootGameObject)
        {
            ValidateMeshCollider(rootGameObject);
        }

        private static void ValidateMeshCollider(GameObject rootGameObject)
        {
            foreach (var meshCollider in rootGameObject.GetComponentsInChildren<MeshCollider>())
            {
                if (!meshCollider.convex)
                {
                    // NOTE: Convex でない MeshCollider が Rigidbody の子孫にある場合は、これを弾く。
                    //       Convex でない MeshCollider は Rigidbody 以下では意味をなさないため。
                    //       またその場合、間違った理解のもと使っている可能性が高い。
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
