public static class GameSession
{
	public static KahootData SelectedKahoot { get; private set; }

	public static void SetSelectedKahoot(KahootData kahoot)
	{
		SelectedKahoot = kahoot;
	}
}

//este script lo que hace es guardar que kahoot hemos seleccionado