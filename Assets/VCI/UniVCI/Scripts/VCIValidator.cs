using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using VCIGLTF;

namespace VCI
{
    public static class VCIValidator
    {
        private const int MaxSpringBoneCount = 1;
        private const int MaxRootBoneCount = 10;
        private const int MaxChildBoneCount = 10;
        private const int MaxSpringBoneColliderCount = 10;
        private const int MaxSphereColliderCount = 10;

        private const float SpawnPointAllowedHeightRange = 0.001f;

        public const int VersionTextLength = 30;
        public const int AuthorTextLength = 30;
        public const int ContactInformationTextLength = 255;
        public const int ReferenceTextLength = 255;
        public const int TitleTextLength = 30;
        public const int DescriptionTextLength = 500;
        public const int ModelDataOtherLicenseUrlLength = 2048;
        public const int ScriptOtherLicenseUrlLength = 2048;

        public static void ValidateVCIObject(VCIObject vo)
        {
            // VCIObject
            var vciObjectCount = 0;
            var gameObjectCount = 0;
            var playerSpawnPointsList = new List<VCIPlayerSpawnPoint>();
            var locationBoundsList = new List<VCILocationBounds>();

            foreach (var t in vo.transform.Traverse())
            {
                if (t.GetComponent<VCIObject>() != null) vciObjectCount++;

                var psp = t.GetComponent<VCIPlayerSpawnPoint>();
                if (psp != null) playerSpawnPointsList.Add(psp);

                var locationBounds = t.GetComponent<VCILocationBounds>();
                if (locationBounds != null) locationBoundsList.Add(locationBounds);

                gameObjectCount++;
            }

            if (vciObjectCount > 1)
                throw new VCIValidatorException(ValidationErrorType.MultipleVCIObject);

            // Scripts
            if(vo.Scripts.Any())
            {
                if (vo.Scripts[0].name != "main")
                {
                    throw new VCIValidatorException(ValidationErrorType.FirstScriptNameNotValid);
                }

                var empties = vo.Scripts.Where(x => string.IsNullOrEmpty(x.name));
                if (empties.Any())
                {
                    throw new VCIValidatorException(ValidationErrorType.NoScriptName);
                }

                var duplicates = vo.Scripts.GroupBy(script => script.name)
                    .Where(name => name.Count() > 1)
                    .Select(group => group.Key).ToList();
                if (duplicates.Any())
                {
                    throw new VCIValidatorException(ValidationErrorType.ScriptNameConfliction);
                }

                var invalidChars = Path.GetInvalidFileNameChars().Concat(new []{ '.' }).ToArray();
                foreach (var script in vo.Scripts)
                {
                    if (script.name.IndexOfAny(invalidChars) >= 0)
                    {
                        throw new VCIValidatorException(ValidationErrorType.InvalidCharacter,
                            string.Format(VCIConfig.GetText($"error{(int)ValidationErrorType.InvalidCharacter}"), script.name));
                    }
                };
            }

            VCIMetaValidator.Validate(vo);

            // Invalid Components
            CheckInvalidComponent<MeshCollider>(vo.gameObject);

            // Spring Bone
            var springBones = vo.GetComponents<VCISpringBone>();

            if (springBones != null && springBones.Length > 0) ValidateSpringBones(springBones);

            // PlayerSpawnPoint
            foreach (var psp in playerSpawnPointsList)
            {
                var pspT = psp.gameObject.transform;
                if (Math.Abs(pspT.position.y) > SpawnPointAllowedHeightRange)
                {
                    throw new VCIValidatorException(ValidationErrorType.SpawnPointHeightRangeNotAllowed,
                        VCIConfig.GetText($"error{(int)ValidationErrorType.SpawnPointHeightRangeNotAllowed}"));
                }

                if (Math.Abs(pspT.rotation.x) > 0.001f || Math.Abs(pspT.rotation.z) > 0.001f)
                {
                    throw new VCIValidatorException(ValidationErrorType.SpawnPointNotHorizontal,
                        VCIConfig.GetText($"error{(int)ValidationErrorType.SpawnPointNotHorizontal}"));
                }

                var pspR = psp.GetComponent<VCIPlayerSpawnPointRestriction>();
                if (pspR == null) continue;

                if (pspR.LimitRectLeft > 0
                    || pspR.LimitRectRight < 0
                    || pspR.LimitRectForward < 0
                    || pspR.LimitRectBackward > 0)
                {
                    throw new VCIValidatorException(ValidationErrorType.SpawnPointOriginNotInRange,
                        VCIConfig.GetText($"error{(int)ValidationErrorType.SpawnPointOriginNotInRange}"));
                }

            }

            // LocationBounds
            if (locationBoundsList.Count >= 2)
            {
                throw new VCIValidatorException(ValidationErrorType.LocationBoundsCountLimitOver,
                    VCIConfig.GetText($"error{(int)ValidationErrorType.LocationBoundsCountLimitOver}"));
            }
            if (locationBoundsList.Count > 0)
            {
                var locationBounds = locationBoundsList[0];
                var min = locationBounds.Bounds.min;
                var max = locationBounds.Bounds.max;

                // 10000fを超えてないかだけチェック
                if (Mathf.Abs(min.x) > 10000f || Mathf.Abs(min.y) > 10000f || Mathf.Abs(min.z) > 10000f ||
                    Mathf.Abs(max.x) > 10000f || Mathf.Abs(max.y) > 10000f || Mathf.Abs(max.z) > 10000f)
                {
                    throw new VCIValidatorException(ValidationErrorType.LocationBoundsValueExceeded,
                        VCIConfig.GetText($"error{(int)ValidationErrorType.LocationBoundsValueExceeded}"));
                }
            }
        }

        private static void ValidateSpringBones(VCISpringBone[] targets)
        {
            if (targets.Length > MaxSpringBoneCount)
            {
                throw new VCIValidatorException(ValidationErrorType.TooManySpringBone);
            }

            foreach (var t in targets)
            {
                // Check RootBones
                var rbs = t.RootBones;
                if (rbs == null || rbs.Count == 0)
                    throw new VCIValidatorException(ValidationErrorType.RootBoneNotFound);
                if (rbs.Count > MaxRootBoneCount)
                    throw new VCIValidatorException(ValidationErrorType.TooManyRootBone);

                for (var i = 0; i < rbs.Count; i++)
                {
                    if (rbs[i] == null) continue;
                    var t0 = rbs[i];
                    var childCount = 0;
                    foreach (var t1 in t0.Traverse())
                    {
                        if (t1.GetComponent<VCISubItem>() != null)
                            throw new VCIValidatorException(ValidationErrorType.RootBoneContainsSubItem);
                        childCount++;
                        if (childCount > MaxChildBoneCount)
                            throw new VCIValidatorException(ValidationErrorType.TooManyRootBoneChild);

                        for (var j = 0; j < rbs.Count; j++)
                        {
                            if (j == i) continue;
                            if (rbs[j] == t1)
                                throw new VCIValidatorException(ValidationErrorType.RootBoneNested);
                        }
                    }
                }
            }
        }

        private static void CheckInvalidComponent<T>(GameObject target)
        {
            var c = target.GetComponentsInChildren<T>(true);
            if (c == null || c.Length == 0) return;

            var errorText =
                string.Format(
                    VCIConfig.GetText($"error{(int) ValidationErrorType.InvalidComponent}"),
                    typeof(T).Name);

            throw new VCIValidatorException(ValidationErrorType.InvalidComponent, errorText);
        }

    }

    public enum ValidationErrorType
    {
        // Export menu
        GameObjectNotSelected = 100,
        MultipleSelection = 101,
        VCIObjectNotAttached = 102,

        // VCIObject
        FirstScriptNameNotValid = 200,
        NoScriptName = 201,
        ScriptNameConfliction = 202,
        InvalidCharacter = 203,
        InvalidMetaData = 204,
        MultipleVCIObject = 205,
        InvalidComponent = 206,

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
        SpawnPointHeightRangeNotAllowed = 501,
        SpawnPointNotHorizontal = 502,
        SpawnPointOriginNotInRange = 503,

        // LocationBounds
        LocationBoundsCountLimitOver = 601,
        LocationBoundsValueExceeded = 602,
    }

    [Serializable]
    public class VCIValidatorException: Exception
    {
        public ValidationErrorType ErrorType { get; }

        public VCIValidatorException() : base() {}

        public VCIValidatorException(ValidationErrorType errorType): base("")
        {
            ErrorType = errorType;
        }

        public VCIValidatorException(ValidationErrorType errorType, string message): base(message)
        {
            ErrorType = errorType;
        }

        public VCIValidatorException(string message): base(message) {}

        public VCIValidatorException(string message, Exception innerException)
            : base(message, innerException) {}

        protected VCIValidatorException(SerializationInfo info, StreamingContext context)
            : base(info, context) {}
    }
}
