public static class PlayerData
{
    public static string PlayerName { get; private set; }

    public static void SetPlayerName(string name)
    {
        PlayerName = name;
    }
}

