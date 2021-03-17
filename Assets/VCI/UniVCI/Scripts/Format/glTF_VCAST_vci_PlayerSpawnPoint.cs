using System;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.player_restriction.spawn_point")]
    public class glTF_VCAST_vci_PlayerSpawnPoint
    {
        public static string ExtensionName => "VCAST_vci_PlayerSpawnPoint";

        [UniGLTF.JsonSchema(Description = "Players appear in ascending order from order1. If there are no corresponding orders, they will appear from order0.")]
        public int order;

        [UniGLTF.JsonSchema(Description = "Players appear at positions randomly scattered on the horizontal plane by this amount from the set position.")]
        public float radius;

        public static glTF_VCAST_vci_PlayerSpawnPoint Create(VCIPlayerSpawnPoint psp)
        {
            return new glTF_VCAST_vci_PlayerSpawnPoint
            {
                order = psp.Order,
                radius = psp.Radius
            };
        }
    }

    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.player_restriction.spawn_point.restriction")]
    public class glTF_VCAST_vci_PlayerSpawnPointRestriction
    {
        public const string NoLimit = "NoLimit";
        public const string Circle = "Circle";
        public const string Rectangle = "Rectangle";

        public const string SitOn = "SitOn";

        [UniGLTF.JsonSchema(EnumValues = new object[] {NoLimit, Circle, Rectangle},
            EnumSerializationType = UniGLTF.EnumSerializationType.AsString,
            Description = "Defines the type of the range players can move.")]
        public string rangeOfMovementRestriction;

        [UniGLTF.JsonSchema(Description = "If rangeOfMovementRestriction is Circle, players can move only in the circle of this radius.")]
        public float limitRadius;

        [UniGLTF.JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move to left only by this amount from the origin transform. 0 or less.")]
        public float limitRectLeft;

        [UniGLTF.JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move to right only by this amount from the origin transform. 0 or more.")]
        public float limitRectRight;

        [UniGLTF.JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move forward only by this amount from the origin transform. 0 or more.")]
        public float limitRectForward;

        [UniGLTF.JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move backward only by this amount from the origin transform. 0 or less.")]
        public float limitRectBackward;

        [UniGLTF.JsonSchema(EnumValues = new object[] {NoLimit, SitOn},
            EnumSerializationType = UniGLTF.EnumSerializationType.AsString,
            Description = "Avatars of players spawned at this point is forced to have the set posture.")]
        public string postureRestriction;

        [UniGLTF.JsonSchema(Description = "If postureRestriction is SitOn, the player's avatar will sit on a chair at this height.")]
        public float seatHeight;

        public static glTF_VCAST_vci_PlayerSpawnPointRestriction Create(VCIPlayerSpawnPointRestriction pspR)
        {
            var rangeOfMovementRestriction = "";
            switch (pspR.RangeOfMovementRestriction)
            {
                case RangeOfMovement.NoLimit:
                    rangeOfMovementRestriction = NoLimit;
                    break;
                case RangeOfMovement.Circle:
                    rangeOfMovementRestriction = Circle;
                    break;
                case RangeOfMovement.Rectangle:
                    rangeOfMovementRestriction = Rectangle;
                    break;
            }

            var postureRestriction = "";
            switch (pspR.PostureRestriction)
            {
                case Posture.NoLimit:
                    postureRestriction = NoLimit;
                    break;
                case Posture.SitOn:
                    postureRestriction = SitOn;
                    break;
            }

            return new glTF_VCAST_vci_PlayerSpawnPointRestriction
            {
                rangeOfMovementRestriction = rangeOfMovementRestriction,
                limitRadius = pspR.LimitRadius,
                limitRectLeft = pspR.LimitRectLeft,
                limitRectRight = pspR.LimitRectRight,
                limitRectForward = pspR.LimitRectForward,
                limitRectBackward = pspR.LimitRectBackward,
                postureRestriction = postureRestriction,
                seatHeight = pspR.SeatHeight
            };
        }
    }
}
