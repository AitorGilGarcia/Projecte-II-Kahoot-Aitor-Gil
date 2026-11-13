public static class PlayerData
{
    public static string PlayerName { get; private set; }

    public static void SetPlayerName(string name)
    {
        PlayerName = name;
    }
}

//Debug.Log(PlayerData.PlayerName); para pillar el nombre llamo a playername y ya

// Ej: string playerName = PlayerData.PlayerName;