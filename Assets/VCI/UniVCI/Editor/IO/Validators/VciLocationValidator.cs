using System;
using UnityEngine;

namespace VCI
{
    internal static class VciLocationValidator
    {
        public static void Validate(GameObject gameObject)
        {
            ValidatePlayerSpawnPoints(gameObject);
            ValidateLocationBounds(gameObject);
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
                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.SpawnPointNotHorizontal}");
                    throw new VciValidatorException(VciValidationErrorType.SpawnPointNotHorizontal, playerSpawnPoint, errorText);
                }

                var spawnPointRestriction = playerSpawnPoint.GetComponent<VCIPlayerSpawnPointRestriction>();
                if (spawnPointRestriction == null) continue;

                // Check 2: SpawnPoint が PlayerSpawnPointRestriction で指定した制限範囲内に存在する
                if (spawnPointRestriction.LimitRectLeft > 0
                    || spawnPointRestriction.LimitRectRight < 0
                    || spawnPointRestriction.LimitRectForward < 0
                    || spawnPointRestriction.LimitRectBackward > 0)
                {
                    var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.SpawnPointOriginNotInRange}");
                    throw new VciValidatorException(VciValidationErrorType.SpawnPointOriginNotInRange, spawnPointRestriction, errorText);
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
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.LocationBoundsCountLimitOver}");
                throw new VciValidatorException(VciValidationErrorType.LocationBoundsCountLimitOver, errorText);
            }

            var locationBounds = locationBoundsList[0];
            var min = locationBounds.Bounds.min;
            var max = locationBounds.Bounds.max;

            // Check 2: x, y, z の制限範囲が ±10000 に収まる
            if (Mathf.Abs(min.x) > 10000f || Mathf.Abs(min.y) > 10000f || Mathf.Abs(min.z) > 10000f ||
                Mathf.Abs(max.x) > 10000f || Mathf.Abs(max.y) > 10000f || Mathf.Abs(max.z) > 10000f)
            {
                var errorText = VCIConfig.GetText($"error{(int)VciValidationErrorType.LocationBoundsValueExceeded}");
                throw new VciValidatorException(VciValidationErrorType.LocationBoundsValueExceeded, locationBounds, errorText);
            }
        }
    }
}
