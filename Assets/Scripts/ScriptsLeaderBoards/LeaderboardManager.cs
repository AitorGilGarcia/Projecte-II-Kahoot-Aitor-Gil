
using System.Linq;
using TMPro;
using UnityEngine;

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
		panelLeaderboard.SetActive(false);
		panelSeleccionKahoot.SetActive(true);
	}
}
