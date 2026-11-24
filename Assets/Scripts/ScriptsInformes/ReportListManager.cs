using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Linq;

// busca los .txt de la carpeta Reports y genera los prefabs.

//El ReportListManager hará algo parecido a esto
//1.Buscar todos los .txt de Reports
//2. Instanciar un prefab por cada archivo
//3. Al pulsar un prefab, abrir el segundo panel
//4. Leer el archivo con File.ReadAllText()
//5. Mostrar su contenido en un TMP_Text


public class ReportListManager : MonoBehaviour
{
    [Header("Lista de informes")]
    [SerializeField] private Transform contentParent;
    [SerializeField] private GameObject reportItemPrefab;

    [Header("Paneles")]
    [SerializeField] private GameObject panelListaInformes;
    [SerializeField] private GameObject panelInformeErrores;

    [Header("Contenido del informe")]
    [SerializeField] private TMP_Text tituloInforme;
    [SerializeField] private TMP_Text contenidoInforme;

    private string reportsFolder;

    private void Start()
    {
        reportsFolder = Path.Combine(Application.persistentDataPath, "Reports");

        panelListaInformes.SetActive(true);
        panelInformeErrores.SetActive(false);

        LoadReports();
    }

    private void LoadReports()
    {
        // Limpiar elementos anteriores.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Si no existe la carpeta, no hay informes.
        if (!Directory.Exists(reportsFolder))
        {
            Debug.Log("Todavía no existe la carpeta Reports.");
            return;
        }

        // Buscar todos los archivos TXT.
        string[] files = Directory.GetFiles(
            reportsFolder,
            "*.txt"
        );

        // Mostrar primero los informes más recientes.
        files = files
            .OrderByDescending(File.GetLastWriteTime)
            .ToArray();

        foreach (string filePath in files)
        {
            string fileName = Path.GetFileName(filePath);

            string date = File.GetLastWriteTime(filePath)
                .ToString("dd/MM/yyyy HH:mm:ss");

            GameObject newItem = Instantiate(
                reportItemPrefab,
                contentParent
            );

            ReportItemUI itemUI =
                newItem.GetComponent<ReportItemUI>();

            if (itemUI != null)
            {
                itemUI.Setup(fileName, date, ShowReport);
            }
            else
            {
                Debug.LogError(
                    "El prefab no tiene ReportItemUI."
                );

                Destroy(newItem);
            }
        }

        Debug.Log("Informes encontrados: " + files.Length);
    }

    private void ShowReport(string fileName)
    {
        try
        {
            string filePath = Path.Combine(
                reportsFolder,
                fileName
            );

            string content = File.ReadAllText(filePath);

            tituloInforme.text = fileName;
            contenidoInforme.text = content;

            panelListaInformes.SetActive(false);
            panelInformeErrores.SetActive(true);
        }
        catch (Exception e)
        {
            Debug.LogError(
                "Error leyendo el informe: " + e.Message
            );
        }
    }

    public void BackToList()
    {
        panelInformeErrores.SetActive(false);
        panelListaInformes.SetActive(true);
    }
}