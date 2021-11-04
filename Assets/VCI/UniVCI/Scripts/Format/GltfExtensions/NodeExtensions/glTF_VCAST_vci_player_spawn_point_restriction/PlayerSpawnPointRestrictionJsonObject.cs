using System;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.player_restriction.spawn_point.restriction")]
    public class PlayerSpawnPointRestrictionJsonObject
    {
        public const string NoLimitRangeTypeString = "NoLimit";
        public const string CircleRangeTypeString = "Circle";
        public const string RectangleRangeTypeString = "Rectangle";

        public const string NoLimitPostureRestrictionTypeString = "NoLimit";
        public const string SitOnPostureRestrictionTypeString = "SitOn";

        [UniGLTF.JsonSchema(Description = "Defines the type of the range players can move.")]
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

        [UniGLTF.JsonSchema(Description = "Avatars of players spawned at this point is forced to have the set posture.")]
        public string postureRestriction;

        [UniGLTF.JsonSchema(Description = "If postureRestriction is SitOn, the player's avatar will sit on a chair at this height.")]
        public float seatHeight;
    }
}
