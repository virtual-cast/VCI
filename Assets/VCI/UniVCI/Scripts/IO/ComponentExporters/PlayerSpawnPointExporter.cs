using System;
using UnityEngine;

namespace VCI
{
    /// <summary>
    /// PlayerSpawnPoint を Export できる
    /// </summary>
    public static class PlayerSpawnPointExporter
    {
        public static glTF_VCAST_vci_player_spawn_point ExportPlayerSpawnPoint(Transform node)
        {
            var psp = node.GetComponent<VCIPlayerSpawnPoint>();
            if (psp == null)
            {
                return null;
            }

            return new glTF_VCAST_vci_player_spawn_point
            {
                playerSpawnPoint = new PlayerSpawnPointJsonObject
                {
                    order = psp.Order,
                    radius = psp.Radius,
                },
            };
        }

        public static glTF_VCAST_vci_player_spawn_point_restriction ExportPlayerSpawnPointRestriction(Transform node)
        {
            var psp = node.GetComponent<VCIPlayerSpawnPoint>();
            var pspR = node.GetComponent<VCIPlayerSpawnPointRestriction>();
            if (psp == null || pspR == null)
            {
                return null;
            }

            return new glTF_VCAST_vci_player_spawn_point_restriction
            {
                playerSpawnPointRestriction = new PlayerSpawnPointRestrictionJsonObject
                {
                    rangeOfMovementRestriction = ExportRangeOfMovementRestriction(pspR.RangeOfMovementRestriction),
                    limitRadius = pspR.LimitRadius,
                    limitRectLeft = pspR.LimitRectLeft,
                    limitRectRight = pspR.LimitRectRight,
                    limitRectForward = pspR.LimitRectForward,
                    limitRectBackward = pspR.LimitRectBackward,
                    postureRestriction = ExportPostureRestriction(pspR.PostureRestriction),
                    seatHeight = pspR.SeatHeight,
                },
            };
        }

        private static string ExportRangeOfMovementRestriction(RangeOfMovement type)
        {
            switch (type)
            {
                case RangeOfMovement.NoLimit:
                    return PlayerSpawnPointRestrictionJsonObject.NoLimitRangeTypeString;
                case RangeOfMovement.Circle:
                    return PlayerSpawnPointRestrictionJsonObject.CircleRangeTypeString;
                case RangeOfMovement.Rectangle:
                    return PlayerSpawnPointRestrictionJsonObject.RectangleRangeTypeString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static string ExportPostureRestriction(Posture type)
        {
            switch (type)
            {
                case Posture.NoLimit:
                    return PlayerSpawnPointRestrictionJsonObject.NoLimitPostureRestrictionTypeString;
                case Posture.SitOn:
                    return PlayerSpawnPointRestrictionJsonObject.SitOnPostureRestrictionTypeString;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}