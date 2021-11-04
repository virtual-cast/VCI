using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UniGLTF;
using UnityEditor;

namespace VCI
{
    public static class VCIValidator
    {
        public static void ValidateVCIRequirements(GameObject gameObject)
        {
            ValidateVCIObjectComponentRestrictions(gameObject);

            var vciObject = gameObject.GetComponent<VCIObject>();
            ValidateVCIScripts(vciObject);
            VCIMetaValidator.Validate(vciObject);
            ValidateColliders(gameObject);
            ValidateAnimation(gameObject);
            ValidateSpringBones(gameObject);
            ValidatePlayerSpawnPoints(gameObject);
            ValidateLocationBounds(gameObject);
        }

        private static void ValidateVCIObjectComponentRestrictions(GameObject gameObject)
        {
            // Check 1: RootのGameObjectにVCIObjectがアタッチされている
            var vciObject = gameObject.GetComponent<VCIObject>();
            if (vciObject == null)
            {
                if (vciObject == null)
                {
                    var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.VCIObjectNotAttached}");
                    throw new VCIValidatorException(ValidationErrorType.VCIObjectNotAttached, errorText);
                }
            }

            // Check 2:「VCIObject」コンポーネントがVCIの中で一つのみ存在する
            var vciObjectCount = 0;
            var transforms = vciObject.transform.Traverse().ToArray();

            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCIObject>() == null) { continue; }

                vciObjectCount++;
                if (vciObjectCount > 1)
                {
                    var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.MultipleVCIObject}");
                    throw new VCIValidatorException(ValidationErrorType.MultipleVCIObject, errorText);
                }
            }

            // Check 3: SubItemはVCIObjectと同じGameObjectにアタッチできない・ネストできない
            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCISubItem>() == null) { continue; }
                if (transform.GetComponent<VCIObject>() != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.RootSubItem}", transform.name);
                    throw new VCIValidatorException(ValidationErrorType.RootSubItem, errorText);
                }
                if (transform.parent.parent != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.NestedSubItem}", transform.name);
                    throw new VCIValidatorException(ValidationErrorType.NestedSubItem, errorText);
                }
            }

            // Check 4: AudioSourceはVCIObjectと同じGameObjectにアタッチできない
            foreach (var transform in transforms)
            {
                if (transform.GetComponent<VCIObject>() == null) { continue; }
                if (transform.GetComponent<AudioSource>() != null)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.RootAudioSource}", transform.name);
                    throw new VCIValidatorException(ValidationErrorType.RootAudioSource, errorText);
                }
            }

            // Check 5: VCIObject以下に最低1つはGameObjectがある
            if (vciObject.transform.childCount == 0)
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.OnlyVciObject}");
                throw new VCIValidatorException(ValidationErrorType.OnlyVciObject, errorText);
            }
        }

        private static void ValidateVCIScripts(VCIObject vciObject)
        {
            if (!vciObject.Scripts.Any())
            {
                return;
            }

            // Check 1: 一つ目のスクリプトの名前が「main」である
            if (vciObject.Scripts[0].name != "main")
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.FirstScriptNameNotValid}");
                throw new VCIValidatorException(ValidationErrorType.FirstScriptNameNotValid, errorText);
            }

            // Check 2: 名前が空のスクリプトが存在しない
            var empties = vciObject.Scripts.Where(x => string.IsNullOrEmpty(x.name));
            if (empties.Any())
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.NoScriptName}");
                throw new VCIValidatorException(ValidationErrorType.NoScriptName, errorText);
            }

            // Check 3: 同一の名前のスクリプトが複数存在しない
            var duplicates = vciObject.Scripts.GroupBy(script => script.name)
                .Where(name => name.Count() > 1)
                .Select(group => @group.Key).ToList();
            if (duplicates.Any())
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.ScriptNameConfliction}");
                throw new VCIValidatorException(ValidationErrorType.ScriptNameConfliction, errorText);
            }

            // Check 4: スクリプト名に無効な文字列が含まれていない
            // - 無効な文字列 : ファイル名に含めることのできない文字 + '.'
            var invalidChars = Path.GetInvalidFileNameChars().Concat(new[] { '.' }).ToArray();
            foreach (var script in vciObject.Scripts)
            {
                if (script.name.IndexOfAny(invalidChars) >= 0)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.InvalidCharacter}", script.name);
                    throw new VCIValidatorException(ValidationErrorType.InvalidCharacter, errorText);
                }
            }
        }

        // NOTE:
        // 現状、MeshColliderは使えない
        // - VCI が完全に静的であることが保証できないため
        // - BoxCollider, SphereCollider, CapsuleColliderのみ使用できる
        private static void ValidateColliders(GameObject gameObject)
        {
            // Check 1: MeshCollider が VCI にアタッチされていない
            CheckInvalidComponent<MeshCollider>(gameObject);
        }

        private static void ValidateAnimation(GameObject gameObject)
        {
            // NOTE: Editorコードを含んでいるため、ランタイムでValidateすることができない
            // TODO: Runtime 時にどうするかを考える
#if UNITY_EDITOR
            // Check 1: root の Animator/Animation で自身を animate していない
            // NOTE: root についてる animation の export は UniGLTF 側で行われる
            //       その時、Animator か Animation どちらか片方が一つのみアタッチされていること前提で export される
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

            if (!animationClips.Any())
            {
                return;
            }

            foreach (var animationClip in animationClips)
            {
                foreach (var animationCurveBindings in AnimationUtility.GetCurveBindings(animationClip))
                {
                    if (string.IsNullOrEmpty(animationCurveBindings.path))
                    {
                        var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.RootIsAnimated}");
                        throw new VCIValidatorException(ValidationErrorType.RootIsAnimated, errorText);
                    }
                }
            }
#endif
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
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.TooManySpringBone}");
                throw new VCIValidatorException(ValidationErrorType.TooManySpringBone, errorText);
            }

            const int maxRootBoneCount = 10;
            const int maxChildBoneCount = 10;

            foreach (var springBone in springBones)
            {
                var rootBones = springBone.RootBones;

                // Check: RootBone が存在する
                if (rootBones == null || rootBones.Count == 0)
                {
                    var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.RootBoneNotFound}");
                    throw new VCIValidatorException(ValidationErrorType.RootBoneNotFound, errorText);
                }

                // Check: Root の Bone の数が MaxRootBoneCount 以下である
                if (rootBones.Count > maxRootBoneCount)
                {
                    var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.TooManyRootBone}", maxRootBoneCount, rootBones.Count);
                    throw new VCIValidatorException(ValidationErrorType.TooManyRootBone, errorText);
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
                        var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.TooManyRootBoneChild}", maxChildBoneCount, rootBone.name, children.Length);
                        throw new VCIValidatorException(ValidationErrorType.TooManyRootBoneChild, errorText);
                    }

                    foreach (var childBone in children)
                    {
                        // Check: SpringBone の中に SubItem が存在しない
                        if (childBone.GetComponent<VCISubItem>() != null)
                        {
                            var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.RootBoneContainsSubItem}");
                            throw new VCIValidatorException(ValidationErrorType.RootBoneContainsSubItem, errorText);
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
                                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.RootBoneNested}");
                                throw new VCIValidatorException(ValidationErrorType.RootBoneNested, errorText);
                            }
                        }
                    }
                }
            }
        }

        private static void ValidatePlayerSpawnPoints(GameObject gameObject)
        {
            var playerSpawnPoints = gameObject.GetComponentsInChildren<VCIPlayerSpawnPoint>();

            if (playerSpawnPoints == null)
            {
                return;
            }

            foreach (var playerSpawnPoint in playerSpawnPoints)
            {
                var spawnPointTransform = playerSpawnPoint.gameObject.transform;

                // Check 1: SpawnPoint の向きが水平である
                if (Math.Abs(spawnPointTransform.rotation.x) > 0.001f ||
                    Math.Abs(spawnPointTransform.rotation.z) > 0.001f)
                {
                    var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.SpawnPointNotHorizontal}");
                    throw new VCIValidatorException(ValidationErrorType.SpawnPointNotHorizontal, errorText);
                }

                var spawnPointRestriction = playerSpawnPoint.GetComponent<VCIPlayerSpawnPointRestriction>();
                if (spawnPointRestriction == null) continue;

                // Check 2: SpawnPoint が PlayerSpawnPointRestriction で指定した制限範囲内に存在する
                if (spawnPointRestriction.LimitRectLeft > 0
                    || spawnPointRestriction.LimitRectRight < 0
                    || spawnPointRestriction.LimitRectForward < 0
                    || spawnPointRestriction.LimitRectBackward > 0)
                {
                    var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.SpawnPointOriginNotInRange}");
                    throw new VCIValidatorException(ValidationErrorType.SpawnPointOriginNotInRange, errorText);
                }
            }
        }

        private static void ValidateLocationBounds(GameObject gameObject)
        {
            var locationBoundsList = gameObject.GetComponentsInChildren<VCILocationBounds>();

            if (locationBoundsList == null || locationBoundsList.Length == 0)
            {
                return;
            }

            // Check 1: LocationBounds が一つのみ存在する
            if (locationBoundsList.Length >= 2)
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.LocationBoundsCountLimitOver}");
                throw new VCIValidatorException(ValidationErrorType.LocationBoundsCountLimitOver, errorText);
            }

            var locationBounds = locationBoundsList[0];
            var min = locationBounds.Bounds.min;
            var max = locationBounds.Bounds.max;

            // Check 2: x, y, z の制限範囲が ±10000 に収まる
            if (Mathf.Abs(min.x) > 10000f || Mathf.Abs(min.y) > 10000f || Mathf.Abs(min.z) > 10000f ||
                Mathf.Abs(max.x) > 10000f || Mathf.Abs(max.y) > 10000f || Mathf.Abs(max.z) > 10000f)
            {
                var errorText = VCIConfig.GetText($"error{(int)ValidationErrorType.LocationBoundsValueExceeded}");
                throw new VCIValidatorException(ValidationErrorType.LocationBoundsValueExceeded, errorText);
            }
        }

        private static void CheckInvalidComponent<T>(GameObject target)
        {
            var c = target.GetComponentsInChildren<T>(true);
            if (c == null || c.Length == 0) return;

            var errorText = VCIConfig.GetFormattedText($"error{(int)ValidationErrorType.InvalidComponent}", typeof(T).Name);

            throw new VCIValidatorException(ValidationErrorType.InvalidComponent, errorText);
        }
    }

    public enum ValidationErrorType
    {
        // Export menu
        GameObjectNotSelected = 100,
        MultipleSelection = 101,
        VCIObjectNotAttached = 102,
        OnlyVciObject = 103,

        // VCIObject
        FirstScriptNameNotValid = 200,
        NoScriptName = 201,
        ScriptNameConfliction = 202,
        InvalidCharacter = 203,
        InvalidMetaData = 204,
        MultipleVCIObject = 205,
        InvalidComponent = 206,
        NestedSubItem = 207,
        RootSubItem = 208,
        RootAudioSource = 209,

        // SpringBone
        TooManySpringBone = 400,
        RootBoneNotFound = 401,
        TooManyRootBone = 402,
        TooManyRootBoneChild = 403,
        RootBoneContainsSubItem = 404,
        RootBoneNested = 405,

        // SpringBoneCollider
        TooManySpringBoneCollider = 410,
        TooManySphereCollider = 411,

        // PlayerSpawnPoint
        SpawnPointNotHorizontal = 501,
        SpawnPointOriginNotInRange = 502,

        // LocationBounds
        LocationBoundsCountLimitOver = 601,
        LocationBoundsValueExceeded = 602,

        // Animation
        RootIsAnimated = 701,
    }

    [Serializable]
    public class VCIValidatorException : Exception
    {
        public ValidationErrorType ErrorType { get; }

        public VCIValidatorException() : base() { }

        public VCIValidatorException(ValidationErrorType errorType) : base("")
        {
            ErrorType = errorType;
        }

        public VCIValidatorException(ValidationErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
        }

        public VCIValidatorException(string message) : base(message) { }

        public VCIValidatorException(string message, Exception innerException)
            : base(message, innerException) { }

        protected VCIValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
