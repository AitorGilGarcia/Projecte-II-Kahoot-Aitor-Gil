using UnityEngine;

public class GameManager : MonoBehaviour
{
	private KahootData currentKahoot;

	private void Start()
	{
		currentKahoot = GameSession.SelectedKahoot;

		if (currentKahoot == null)
		{
			Debug.LogError("No se ha seleccionado ningún Kahoot.");
			return;
		}

		Debug.Log("Kahoot cargado: " + currentKahoot.title);
		Debug.Log("Número de preguntas: " + currentKahoot.questions.Count);
	}
}


//este script lo que hace es gestionar la partida