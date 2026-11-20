
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelSeleccionKahoot;
    [SerializeField] private GameObject panelLeaderboard;

    [Header("Datos del leaderboard")]
    [SerializeField] private TMP_Text tituloKahoot;
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject leaderboardItemPrefab;
    [SerializeField] private TMP_Text emptyText;

	private bool returnToGameAfterLeaderboard = false;

	private void Start() // comprobamos si esta entrando despues de la partida o si simplemente va a entrar por el menu principal
	{
		if (GameSession.ConsumeDirectLeaderboardRequest()
			&& GameSession.SelectedKahoot != null)
		{
			returnToGameAfterLeaderboard = true;
			ShowLeaderboard(GameSession.SelectedKahoot);
		}
		else
		{
			returnToGameAfterLeaderboard = false;

			panelSeleccionKahoot.SetActive(true);
			panelLeaderboard.SetActive(false);
		}
	}

	public void ShowLeaderboard(KahootData kahoot)
	{
		// Ocultamos la selección de Kahoots.
		panelSeleccionKahoot.SetActive(false);

		// Mostramos el leaderboard.
		panelLeaderboard.SetActive(true);

		// Ponemos el título del Kahoot seleccionado.
		tituloKahoot.text = kahoot.title;


        // Eliminar los elementos anteriores.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Leer los resultados del XML.
        LeaderboardData data = LeaderboardSaveSystem.LoadResults(kahoot);

        // Ordenar por puntuación y después por tiempo.
        var results = data.results.OrderByDescending(r => r.score).ThenBy(r => r.totalTime).ToList();

        // Mostrar mensaje si no hay resultados.
        bool hasResults = results.Count > 0;

        if (emptyText != null) emptyText.gameObject.SetActive(!hasResults);

        // Crear un prefab por cada resultado.
        for (int i = 0; i < results.Count; i++)
        {
            GameObject newItem = Instantiate(leaderboardItemPrefab, contentParent);

            LeaderboardItemUI itemUI = newItem.GetComponent<LeaderboardItemUI>();

            if (itemUI != null)
            {
                itemUI.Setup(results[i], i + 1);
            }
            else
            {
                Debug.LogError( "El prefab no tiene LeaderboardItemUI.");
            }
        }
    }

	public void BackToSelection()
	{
		// Si hemos entrado desde una partida, volvemos a sus resultados.
		if (returnToGameAfterLeaderboard)
		{
			GameSession.RequestShowResultsOnReturn();
			UnityEngine.SceneManagement.SceneManager.LoadScene("EscenaGame");
		}
		else
		{
			// Si hemos entrado desde el menú, volvemos al selector.
			panelLeaderboard.SetActive(false);
			panelSeleccionKahoot.SetActive(true);
		}
	}
}
