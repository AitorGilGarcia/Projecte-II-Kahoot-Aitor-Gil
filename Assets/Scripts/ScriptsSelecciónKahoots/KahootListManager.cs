
using UnityEngine;
using System;
using System.IO;

public class KahootListManager : MonoBehaviour
{
    [Header("Configuración de la lista")]
    [SerializeField] private GameObject kahootItemPrefab;
    [SerializeField] private Transform contentParent;

	[Header("Destino")]
	[SerializeField] private bool openLeaderboard;
	[SerializeField] private LeaderboardManager leaderboardManager;


	private void Start()
    {
        LoadKahoots();
    }

    // Busca los JSON y crea un elemento por cada Kahoot
    private void LoadKahoots()
    {
        string folderPath = Path.Combine(Application.persistentDataPath,"Kahoots");

        // Si todavía no existe la carpeta, no hay Kahoots guardados
        if (!Directory.Exists(folderPath))
        {
            Debug.Log("Todavía no existe la carpeta de Kahoots.");
            return;
        }

        string[] files = Directory.GetFiles(folderPath, "*.json");

        foreach (string filePath in files)
        {
            try
            {
                

                // Leemos el contenido del archivo
                string json = File.ReadAllText(filePath);

                // Convertimos el JSON en un objeto KahootData
                KahootData kahoot = JsonUtility.FromJson<KahootData>(json);

				if (kahoot == null || string.IsNullOrWhiteSpace(kahoot.title) || kahoot.questions == null) // esto si el kahoot no existe, no tiene titulo o no tiene preguntas
                                                                                                           // salta la excepcion de que no es corecto el kahoot
				{
					Debug.LogWarning("No se ha podido leer correctamente: " + filePath);

					ReportSaveSystem.SaveReport("Archivo JSON incorrecto", filePath, new Exception
                        ("El archivo no tiene los datos necesarios para crear un Kahoot "));

					continue;
				}

				// Asignamos un ID a los Kahoots antiguos que todavía no lo tienen
				if (string.IsNullOrEmpty(kahoot.id))
                {
                    kahoot.id = Guid.NewGuid().ToString();

                    // Actualizamos el JSON para conservar el identificador
                    File.WriteAllText(filePath, JsonUtility.ToJson(kahoot, true));
                }

                // Creamos una copia del prefab dentro de Content
                GameObject newItem = Instantiate(kahootItemPrefab, contentParent);

                // Le pasamos los datos al componente de interfaz
                KahootItemUI itemUI = newItem.GetComponent<KahootItemUI>();

                if (itemUI != null)
                {
                    itemUI.Setup(kahoot, OnKahootSelected);
                }

                else
                {
                    Debug.LogError("El prefab no tiene KahootItemUI.");
                    Destroy(newItem);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(
                    "Error leyendo el archivo " + filePath + ": " + e.Message
                );

				ReportSaveSystem.SaveReport("Error al cargar un Kahoot", filePath, e); // hacemos la excepción 
			}
        }

        Debug.Log("Kahoots encontrados: " + files.Length);
    }

    // Se ejecuta cuando pulsamos el botón de un Kahoot
    private void OnKahootSelected(KahootData kahoot)
    {
        Debug.Log("Has seleccionado: " + kahoot.title);


        // depende que escena este haciendo, hace una cosa o otra
		if (openLeaderboard)
		{
			leaderboardManager.ShowLeaderboard(kahoot);
		}

        else
        {
			GameSession.SetSelectedKahoot(kahoot); //Guardamos que kahoot hemos seleccionado para jugar

			UnityEngine.SceneManagement.SceneManager.LoadScene("EscenaGame"); // vamos a la escena de donde esta la partida 
		}
		
	}
}
