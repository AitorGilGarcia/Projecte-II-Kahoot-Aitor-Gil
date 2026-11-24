using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour // este script lo que hace es mandar el nombre del player y despues empezar la partida
{
    [SerializeField] private TMP_InputField nameInput;
	[SerializeField] private GameManager gameManager;

    private void Start()
    {
        // Limitamos el nombre del jugador a 15 caracteres
        nameInput.characterLimit = 15;
    }

    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
        {
            Debug.LogWarning("Introduce un nombre.");
            return;
        }

        PlayerData.SetPlayerName(nameInput.text.Trim());
        Debug.Log(PlayerData.PlayerName);

		// Iniciamos la partida después de guardar el nombre llamando a gamemanager
		gameManager.StartGame();
	}
}