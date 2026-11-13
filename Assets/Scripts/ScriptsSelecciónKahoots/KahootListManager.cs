
using UnityEngine;
using System;
using System.IO;

public class KahootListManager : MonoBehaviour
{
    [Header("Configuración de la lista")]
    [SerializeField] private GameObject kahootItemPrefab;
    [SerializeField] private Transform contentParent;

    private void Start()
    {
        LoadKahoots();
    }

    // Busca los JSON y crea un elemento por cada Kahoot.
    private void LoadKahoots()
    {
        string folderPath = Path.Combine(Application.persistentDataPath,"Kahoots");

        // Si todavía no existe la carpeta, no hay Kahoots guardados.
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
                // Leemos el contenido del archivo.
                string json = File.ReadAllText(filePath);

                // Convertimos el JSON en un objeto KahootData.
                KahootData kahoot = JsonUtility.FromJson<KahootData>(json);

                if (kahoot == null)
                {
                    Debug.LogWarning("No se ha podido leer: " + filePath);
                    continue;
                }

                // Creamos una copia del prefab dentro de Content.
                GameObject newItem = Instantiate(kahootItemPrefab, contentParent);

                // Le pasamos los datos al componente de interfaz.
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
            }
        }

        Debug.Log("Kahoots encontrados: " + files.Length);
    }

    // Se ejecuta cuando pulsamos el botón de un Kahoot.
    private void OnKahootSelected(KahootData kahoot)
    {
        Debug.Log("Has seleccionado: " + kahoot.title);

        // Más adelante conectaremos aquí la selección
        // con la escena de juego.
    }
}
