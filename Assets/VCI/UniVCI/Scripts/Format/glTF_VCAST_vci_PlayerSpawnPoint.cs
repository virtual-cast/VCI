using System;
using VCIGLTF;
using VCIJSON;

namespace VCI
{
    [Serializable]
    [JsonSchema(Title = "vci.player_restriction.spawn_point")]
    public class glTF_VCAST_vci_PlayerSpawnPoint : JsonSerializableBase
    {
        [JsonSchema(Description = "Players appear in ascending order from order1. If there are no corresponding orders, they will appear from order0.")]
        public int order;

        [JsonSchema(Description = "Players appear at positions randomly scattered on the horizontal plane by this amount from the set position.")]
        public float radius;

        protected override void SerializeMembers(GLTFJsonFormatter f)
        {
            f.KeyValue(() => order);
            f.KeyValue(() => radius);
        }

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
    [JsonSchema(Title = "vci.player_restriction.spawn_point.restriction")]
    public class glTF_VCAST_vci_PlayerSpawnPointRestriction : JsonSerializableBase
    {
        public const string NoLimit = "NoLimit";
        public const string Circle = "Circle";
        public const string Rectangle = "Rectangle";

        public const string SitOn = "SitOn";

        [JsonSchema(EnumValues = new object[] {NoLimit, Circle, Rectangle},
            EnumSerializationType = EnumSerializationType.AsString,
            Description = "Defines the type of the range players can move.")]
        public string rangeOfMovementRestriction;

        [JsonSchema(Description = "If rangeOfMovementRestriction is Circle, players can move only in the circle of this radius.")]
        public float limitRadius;

        [JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move to left only by this amount from the origin transform. 0 or less.")]
        public float limitRectLeft;

        [JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move to right only by this amount from the origin transform. 0 or more.")]
        public float limitRectRight;

        [JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move forward only by this amount from the origin transform. 0 or more.")]
        public float limitRectForward;

        [JsonSchema(Description = "If rangeOfMovementRestriction is Rectangle, players can move backward only by this amount from the origin transform. 0 or less.")]
        public float limitRectBackward;

        [JsonSchema(EnumValues = new object[] {NoLimit, SitOn},
            EnumSerializationType = EnumSerializationType.AsString,
            Description = "Avatars of players spawned at this point is forced to have the set posture.")]
        public string postureRestriction;

        protected override void SerializeMembers(GLTFJsonFormatter f)
        {
            f.KeyValue(() => rangeOfMovementRestriction);

            f.KeyValue(() => limitRadius);
            f.KeyValue(() => limitRectLeft);
            f.KeyValue(() => limitRectRight);
            f.KeyValue(() => limitRectForward);
            f.KeyValue(() => limitRectBackward);

            f.KeyValue(() => postureRestriction);
        }

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
                postureRestriction = postureRestriction
            };
        }
    }
}
