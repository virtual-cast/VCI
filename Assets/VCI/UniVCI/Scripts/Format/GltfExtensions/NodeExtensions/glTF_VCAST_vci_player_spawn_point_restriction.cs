using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_player_spawn_point_restriction
    {
        public static string ExtensionName => "VCAST_vci_player_spawn_point_restriction";

        public PlayerSpawnPointRestrictionJsonObject playerSpawnPointRestriction;
    }
}
