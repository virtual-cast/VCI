using System.Linq;
using UniGLTF;
using UnityEngine;

namespace VCI
{
    internal static class VciSpringBoneValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateSpringBones(gameObject);
        }

        private static void ValidateSpringBones(GameObject gameObject)
        {
            // NOTE: どういう歴史的経緯があったのか分からないけど、現在これらの使われていない
            // const int maxSpringBoneColliderCount = 10;
            // const int maxSphereColliderCount = 10;

            var springBones = gameObject.GetComponents<VCISpringBone>();

            if (springBones == null)
            {
                return;
            }

            // Check: アタッチされている SpringBone コンポーネントが maxSpringBoneCount以下である
            const int maxSpringBoneCount = 1;
            if (springBones.Length > maxSpringBoneCount)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.TooManySpringBone}");
                throw new VciValidatorException(VciValidationErrorType.TooManySpringBone, errorText);
            }

            const int maxRootBoneCount = 10;
            const int maxChildBoneCount = 10;

            foreach (var springBone in springBones)
            {
                var rootBones = springBone.RootBones;

                // Check: RootBone が存在する
                if (rootBones == null || rootBones.Count == 0)
                {
                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RootBoneNotFound}");
                    throw new VciValidatorException(VciValidationErrorType.RootBoneNotFound, springBone, errorText);
                }

                // Check: Root の Bone の数が MaxRootBoneCount 以下である
                if (rootBones.Count > maxRootBoneCount)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.TooManyRootBone}", maxRootBoneCount, rootBones.Count);
                    throw new VciValidatorException(VciValidationErrorType.TooManyRootBone, springBone, errorText);
                }

                for (var i = 0; i < rootBones.Count; i++)
                {
                    if (rootBones[i] == null)
                    {
                        continue;
                    }

                    // Check: RootBone の持つ ChildBone が MaxChildBoneCount 以下である
                    var rootBone = rootBones[i];
                    var children = rootBone.Traverse().ToArray();
                    if (children.Length > maxChildBoneCount)
                    {
                        var errorText = VCIConfig.GetFormattedText($"error{(int)VciValidationErrorType.TooManyRootBoneChild}", maxChildBoneCount, rootBone.name, children.Length);
                        throw new VciValidatorException(VciValidationErrorType.TooManyRootBoneChild, springBone, errorText);
                    }

                    foreach (var childBone in children)
                    {
                        // Check: SpringBone の中に SubItem が存在しない
                        if (childBone.GetComponent<VCISubItem>() != null)
                        {
                            var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RootBoneContainsSubItem}");
                            throw new VciValidatorException(VciValidationErrorType.RootBoneContainsSubItem, childBone, errorText);
                        }


                        // Check: RootBone が入れ子になっていない
                        for (var j = 0; j < rootBones.Count; j++)
                        {
                            if (j == i)
                            {
                                continue;
                            }

                            if (rootBones[j] == childBone)
                            {
                                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RootBoneNested}");
                                throw new VciValidatorException(VciValidationErrorType.RootBoneNested, childBone, errorText);
                            }
                        }
                    }
                }
            }
        }

    }
}
