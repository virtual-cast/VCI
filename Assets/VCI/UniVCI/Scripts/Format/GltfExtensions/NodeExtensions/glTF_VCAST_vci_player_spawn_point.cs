using System;

namespace VCI
{
    /// <summary>
    /// Extension root
    /// </summary>
    [Serializable]
    public class glTF_VCAST_vci_player_spawn_point
    {
        public static string ExtensionName => "VCAST_vci_player_spawn_point";

        public PlayerSpawnPointJsonObject playerSpawnPoint;
    }
}
