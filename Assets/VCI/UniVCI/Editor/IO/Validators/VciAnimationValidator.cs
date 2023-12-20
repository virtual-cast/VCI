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
        private static void ValidateAnimation(GameObject vciRoot)
        {
            // Check 1: root の Animator/Animation で自身を animate していない
            // NOTE: root についてる animation の export は UniGLTF 側で行われる
            //       その時、Animator か Animation どちらか片方が一つのみアタッチされていること前提で export される
            CheckSelfGameObjAnimated(vciRoot);

            // Check 2: AnimationClip に使用できない値が含まれていない
            //
            // Check 2-1: rotation の animation の補完方法が Quaternion である
            // NOTE: Unity の Runtime の制約上、Quaternion 以外の回転表現形式をサポートしていない
            //
            // Check 2-2: 存在しない blend shape を参照する animation が含まれていない
            ValidateAnimationClips(vciRoot);
        }

        private static void CheckSelfGameObjAnimated(GameObject vciRoot)
        {
            // NOTE: Editorコードを含んでいるため、ランタイムでValidateすることができない
            // TODO: Runtime 時にどうするかを考える
#if UNITY_EDITOR
            var animationClips = CollectAnimationClips(vciRoot);

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

        private static void ValidateAnimationClips(GameObject vciRoot)
        {
            // NOTE: Editorコードを含んでいるため、ランタイムでValidateすることができない
            // TODO: Runtime 時にどうするかを考える
#if UNITY_EDITOR
            var animationClips = new List<(AnimationClip animationClip, Transform attacedTransform)>();

            var animators = vciRoot.GetComponentsInChildren<Animator>();
            foreach (var animator in animators)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animator).Select(a => (a, animator.transform)));
            }

            var animations = vciRoot.GetComponentsInChildren<Animation>();
            foreach (var animation in animations)
            {
                animationClips.AddRange(AnimationExporter.GetAnimationClips(animation).Select(a => (a, animation.transform)));
            }

            if (!animationClips.Any())
            {
                return;
            }

            var blendShapeNamesCache = new Dictionary<SkinnedMeshRenderer, List<string>>();
            foreach (var (animationClip, animationRoot) in animationClips)
            {
                foreach (var animationCurveBindings in AnimationUtility.GetCurveBindings(animationClip))
                {
                    // Check 2-1: rotation の animation の補完方法が Quaternion である
                    CheckRotationAnimationInterpolation(animationCurveBindings, animationClip);

                    // Check 2-2: 存在しない blend shape を参照する animation が含まれていない
                    CheckBlendShapeExistence(animationCurveBindings, animationRoot, animationClip, blendShapeNamesCache);
                }
            }
#endif
        }

# if UNITY_EDITOR
        private static void CheckRotationAnimationInterpolation(EditorCurveBinding animationCurveBindings, AnimationClip animationClip)
        {
            if (!animationCurveBindings.propertyName.FastStartsWith("localEulerAnglesRaw.")) return;

            var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.RotationInterpolationIsEulerAngles}");
            throw new VciValidatorException(VciValidationErrorType.RotationInterpolationIsEulerAngles, animationClip, errorText);
        }

        private static void CheckBlendShapeExistence(
            EditorCurveBinding animationCurveBindings,
            Transform animationRoot,
            AnimationClip animationClip,
            Dictionary<SkinnedMeshRenderer, List<string>> skinnedMeshBlendShapes)
        {
            void Throw()
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.ContainsMissingBlendShape}");
                throw new VciValidatorException(VciValidationErrorType.ContainsMissingBlendShape, animationClip, errorText);
            }

            if (animationCurveBindings.type != typeof(SkinnedMeshRenderer)) return;

            // Blend Shape Animation の Property Name は "blendShape.{BlendShapeName}" である、という推測に基づいたValidationが実装されている。
            // Unity の仕様変更によって Property Name の形式が変更された場合は LogWarning を出力して Validation をスキップする。
            // そのような仕様変更が行われた場合は Validationの 実装を変更する必要がある。
            if (!animationCurveBindings.propertyName.FastStartsWith("blendShape."))
            {
                Debug.LogWarning("Unknown property name: " + animationCurveBindings.propertyName);
                return;
            }

            var skinnedMeshRendererTransform = animationRoot.Find(animationCurveBindings.path);
            if (!skinnedMeshRendererTransform) Throw();

            var skinnedMeshRenderer = skinnedMeshRendererTransform.GetComponent<SkinnedMeshRenderer>();
            if (!skinnedMeshRenderer) Throw();

            if (!skinnedMeshBlendShapes.TryGetValue(skinnedMeshRenderer, out var blendShapeNames))
            {
                blendShapeNames = GetBlendShapeNamesFromSkinnedMeshRenderer(skinnedMeshRenderer);
                skinnedMeshBlendShapes.Add(skinnedMeshRenderer, blendShapeNames);
            }

            // property name から "blendShape." prefix を取り除く
            var propertyName = animationCurveBindings.propertyName[11..];

            if (!blendShapeNames.Contains(propertyName)) Throw();
        }
#endif

        private static List<string> GetBlendShapeNamesFromSkinnedMeshRenderer(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            var blendShapeNames = new List<string>();
            var mesh = skinnedMeshRenderer.sharedMesh;
            for (var i = 0; i < mesh.blendShapeCount; i++)
            {
                blendShapeNames.Add(mesh.GetBlendShapeName(i));
            }
            return blendShapeNames;
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
