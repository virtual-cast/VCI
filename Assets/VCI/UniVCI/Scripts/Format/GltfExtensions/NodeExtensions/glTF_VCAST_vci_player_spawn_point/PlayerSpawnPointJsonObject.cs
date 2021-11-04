using System;

namespace VCI
{
    [Serializable]
    [UniGLTF.JsonSchema(Title = "vci.player_restriction.spawn_point")]
    public class PlayerSpawnPointJsonObject
    {
        [UniGLTF.JsonSchema(Description = "Players appear in ascending order from order1. If there are no corresponding orders, they will appear from order0.")]
        public int order;

        [UniGLTF.JsonSchema(Description = "Players appear at positions randomly scattered on the horizontal plane by this amount from the set position.")]
        public float radius;
    }
}
