
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
	[Header("Paneles")]
	[SerializeField] private GameObject panelSeleccionKahoot;
	[SerializeField] private GameObject panelLeaderboard;

	[Header("Datos del leaderboard")]
	[SerializeField] private TMP_Text tituloKahoot;

	public void ShowLeaderboard(KahootData kahoot)
	{
		// Ocultamos la selección de Kahoots.
		panelSeleccionKahoot.SetActive(false);

		// Mostramos el leaderboard.
		panelLeaderboard.SetActive(true);

		// Ponemos el título del Kahoot seleccionado.
		tituloKahoot.text = kahoot.title;

		// Más adelante aquí cargaremos su XML.
	}

	public void BackToSelection()
	{
		panelLeaderboard.SetActive(false);
		panelSeleccionKahoot.SetActive(true);
	}
}
