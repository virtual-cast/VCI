using System.Collections.Generic;
using System.Linq;
using UniGLTF;
using UnityEditor;
using UnityEngine;

namespace VCI
{
    internal static class VciAnimationValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidateAnimation(gameObject);
        }

        // NOTE: 引数のgameObjectは、VCIのrootのgameObject（VCIObjectコンポーネントがアタッチされているgameObject）である
        private static void ValidateAnimation(GameObject gameObject)
        {
            // Check 1: root の Animator/Animation で自身を animate していない
            // NOTE: root についてる animation の export は UniGLTF 側で行われる
            //       その時、Animator か Animation どちらか片方が一つのみアタッチされていること前提で export される
            CheckSelfGameObjAnimated(gameObject);

            // Check 2: rotation の animation の補完方法が Quaternion である
            // NOTE: Unity の Runtime の制約上、Quaternion 以外の回転表現形式をサポートしていない
            CheckRotationAnimationInterpolation(gameObject);
        }

        private static void CheckSelfGameObjAnimated(GameObject gameObject)
        {
            // NOTE: Editorコードを含んでいるため、ランタイムでValidateすることができない
            // TODO: Runtime 時にどうするかを考える
#if UNITY_EDITOR
            var animationClips = CollectAnimationClips(gameObject);

            if (!animationClips.Any())
            {
                return;
            }

            foreach (var animationClip in animationClips)
            {
                foreach (var animationCurveBindings in AnimationUtility.GetCurveBindings(animationClip))
                {
                    if (!string.IsNullOrEmpty(animationCurveBindings.path)) continue;

                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RootIsAnimated}");
                    throw new VciValidatorException(VciValidationErrorType.RootIsAnimated, animationClip, errorText);
                }
            }
#endif
        }

        // NOTE: 引数に渡されるgameObjectがVCI rootであることを前提にしている
        private static void CheckRotationAnimationInterpolation(GameObject gameObject)
        {
            // NOTE: Editorコードを含んでいるため、ランタイムでValidateすることができない
            // TODO: Runtime 時にどうするかを考える
#if UNITY_EDITOR
            var animationClips = new List<AnimationClip>();

            var animators = gameObject.GetComponentsInChildren<Animator>();
            foreach (var animator in animators)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animator));
            }

            var animations = gameObject.GetComponentsInChildren<Animation>();
            foreach (var animation in animations)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animation));
            }

            if (!animationClips.Any())
            {
                return;
            }

            foreach (var animationClip in animationClips)
            {
                foreach (var animationCurveBindings in AnimationUtility.GetCurveBindings(animationClip))
                {
                    if (!animationCurveBindings.propertyName.FastStartsWith("localEulerAnglesRaw.")) continue;

                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RotationInterpolationIsEulerAngles}");
                    throw new VciValidatorException(VciValidationErrorType.RotationInterpolationIsEulerAngles, animationClip, errorText);
                }
            }
#endif
        }

        private static List<AnimationClip> CollectAnimationClips(GameObject gameObject)
        {
            var animationClips = new List<AnimationClip>();
            var animator = gameObject.GetComponent<Animator>();
            var animation = gameObject.GetComponent<Animation>();
            if (animator != null)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animator));
            }
            else if (animation != null)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animation));
            }

            return animationClips;
        }
    }
}
