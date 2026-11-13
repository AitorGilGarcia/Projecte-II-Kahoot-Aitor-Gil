using UnityEngine;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
        {
            Debug.LogWarning("Introduce un nombre.");
            return;
        }

        PlayerData.SetPlayerName(nameInput.text.Trim());

        // Aquí iniciaremos la partida.
    }
}