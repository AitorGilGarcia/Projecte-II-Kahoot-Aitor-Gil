
using UnityEngine;
using System;
using System.IO;

public static class KahootSaveSystem // este script convierte los datos en JSON y escribir el archivo.
{
    public static bool Save(KahootData kahoot)
    {
        try
        {
            // Creamos la carpeta donde guardaremos los Kahoots.
            string folderPath = Path.Combine(Application.persistentDataPath,"Kahoots"); //permite guardar archivos en una ubicación persistente de la aplicación,
                                                                                        //fuera de la carpeta de Assets.

            Directory.CreateDirectory(folderPath);

            // Preparamos un nombre válido para el archivo.
            string fileName = kahoot.title.Trim();

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            // Evitamos sobrescribir otros Kahoots.
            fileName += "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string filePath = Path.Combine(
                folderPath,
                fileName + ".json"
            );

            // Convertimos el objeto en JSON.
            string json = JsonUtility.ToJson(kahoot, true); // convierte nuestras clases en JSON.

            // Escribimos el archivo.
            File.WriteAllText(filePath, json);


            // Mostramos la ruta del archivo generado.
            Debug.Log("Kahoot guardado correctamente: " + filePath);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar el Kahoot: " + e.Message);
            return false;
        }
    }
}



