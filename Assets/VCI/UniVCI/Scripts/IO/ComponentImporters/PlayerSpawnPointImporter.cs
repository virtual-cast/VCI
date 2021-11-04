using System.Collections.Generic;
using UnityEngine;

namespace VCI
{
    public static class PlayerSpawnPointImporter
    {
        public static void Load(VciData vciData, IReadOnlyList<Transform> unityNodes)
        {
            var playerSpawnPointNodeIndices = new HashSet<int>();
            foreach (var (nodeIdx, playerSpawnPointExtension) in vciData.PlayerSpawnPointNodes)
            {
                playerSpawnPointNodeIndices.Add(nodeIdx);

                var gameObject = unityNodes[nodeIdx].gameObject;
                var spawnPoint = gameObject.AddComponent<VCIPlayerSpawnPoint>();
                spawnPoint.Order = playerSpawnPointExtension.playerSpawnPoint.order;
                spawnPoint.Radius = playerSpawnPointExtension.playerSpawnPoint.radius;
            }

            foreach (var (nodeIdx, playerSpawnPointRestrictionExtension) in vciData.PlayerSpawnPointRestrictionNodes)
            {
                if (!playerSpawnPointNodeIndices.Contains(nodeIdx)) continue;

                var gameObject = unityNodes[nodeIdx].gameObject;
                var spawnPointRestriction = gameObject.AddComponent<VCIPlayerSpawnPointRestriction>();
                var nodePspR = playerSpawnPointRestrictionExtension.playerSpawnPointRestriction;

                spawnPointRestriction.RangeOfMovementRestriction = DeserializeRangeOfMovementRestriction(nodePspR.rangeOfMovementRestriction);
                spawnPointRestriction.LimitRadius = nodePspR.limitRadius;
                spawnPointRestriction.LimitRectLeft = nodePspR.limitRectLeft;
                spawnPointRestriction.LimitRectRight = nodePspR.limitRectRight;
                spawnPointRestriction.LimitRectForward = nodePspR.limitRectForward;
                spawnPointRestriction.LimitRectBackward = nodePspR.limitRectBackward;
                spawnPointRestriction.PostureRestriction = DeserializePostureRestriction(nodePspR.postureRestriction);
                spawnPointRestriction.SeatHeight = nodePspR.seatHeight;
            }
        }

        private static RangeOfMovement DeserializeRangeOfMovementRestriction(string jsonString)
        {
            switch (jsonString)
            {
                case PlayerSpawnPointRestrictionJsonObject.NoLimitRangeTypeString:
                    return RangeOfMovement.NoLimit;
                case PlayerSpawnPointRestrictionJsonObject.CircleRangeTypeString:
                    return RangeOfMovement.Circle;
                case PlayerSpawnPointRestrictionJsonObject.RectangleRangeTypeString:
                    return RangeOfMovement.Rectangle;
                default: // 無指定の場合、デフォルト値にフォールバックする.
                    return RangeOfMovement.NoLimit;
            }
        }

        private static Posture DeserializePostureRestriction(string jsonString)
        {
            switch (jsonString)
            {
                case PlayerSpawnPointRestrictionJsonObject.NoLimitPostureRestrictionTypeString:
                    return Posture.NoLimit;
                case PlayerSpawnPointRestrictionJsonObject.SitOnPostureRestrictionTypeString:
                    return Posture.SitOn;
                default: // 無指定の場合、デフォルト値にフォールバックする.
                    return Posture.NoLimit;
            }
        }
    }
}