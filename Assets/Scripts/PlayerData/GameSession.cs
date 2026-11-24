public static class GameSession
{
	public static KahootData SelectedKahoot { get; private set; }
	public static bool OpenLeaderboardDirectly { get; private set; }
	public static bool ShowResultsOnReturn { get; private set; }
	public static int FinalScore { get; private set; }
	public static void SetSelectedKahoot(KahootData kahoot) // indicar que kahoot esta seleccionado
	{
		SelectedKahoot = kahoot;
	}
	public static void SetFinalScore(int score)
	{
		FinalScore = score;
	}
	public static void RequestDirectLeaderboard() // indicamos si queremos abrir la leaderboard directamente, esto es para cuando termina la partida, poder ver la leaderboard sin tener que 
												  // seleccioanrla en el panel anterior que hay
	{
		OpenLeaderboardDirectly = true;
	}
	public static bool ConsumeDirectLeaderboardRequest()
	{
		bool requested = OpenLeaderboardDirectly;
		OpenLeaderboardDirectly = false;
		return requested;
	}

	public static void RequestShowResultsOnReturn() // esto lo hacemos con el objetivo de poder volver desde la leaderboard y aparecer otra vez en la pantalla final de la partida
	{
		ShowResultsOnReturn = true;
	}

	public static bool ConsumeShowResultsOnReturn()
	{
		bool requested = ShowResultsOnReturn;
		ShowResultsOnReturn = false;
		return requested;
	}
}

//este script lo que hace es guardar que kahoot hemos seleccionado y gestionar la info de lo de abrir la leaderboard desde el final