using System;
using System.IO;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.PlayerSettings;

// Este script será el encargado de generar y guardar automáticamente los informes de errores en archivos.txt.

public static class ReportSaveSystem // lo hacemos estatico para poder utilizarlo desde donde sea sin tener que asignarlo en ningun sitio 
{
	public static void CreateTestReport() // hace un test de un error
	{
		SaveReport(
			"Error de prueba",
			"archivo_prueba.json",
			new Exception("Esto es una excepción de prueba del sistema de informes.")
		);
	}

	// Guardar todos los informes en esta carpeta.
	private static string GetReportsFolder() // Obtiene la ruta de la carpeta Reports dentro de Application.persistentDataPath.
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Reports");

        Directory.CreateDirectory(folderPath); // si no existe la carpeta, la crea

        return folderPath;
    }

    // Generar y guardar un informe de error.
    public static bool SaveReport(string errorType, string affectedFile, Exception exception)  // este es el metodo que se utiliza en los otros scripts, recibe tres inputs,
                                                                                               // errorType: Tipo de error que se ha producido.
                                                                                               // affectedFile: Archivo que ha provocado el problema.
                                                                                               // exception	Información de la excepción capturada.

    {
        try
        {
            string folderPath = GetReportsFolder();

            // Nombre del archivo con fecha y hora.
            string fileName = "Error_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") +
                ".txt";

            string filePath = Path.Combine(folderPath, fileName);

            // Obtener fecha y hora.
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string time = DateTime.Now.ToString("HH:mm:ss");

            // Preparar el contenido del informe.
            StringBuilder report = new StringBuilder();

            report.AppendLine("INFORME DE ERROR");
            report.AppendLine("==============================");
            report.AppendLine();

            report.AppendLine("Fecha: " + date);
            report.AppendLine("Hora: " + time);
            report.AppendLine();

            report.AppendLine("Tipo de error: " + errorType);
            report.AppendLine("Archivo afectado: " + affectedFile);
            report.AppendLine();

            report.AppendLine("Descripción:");
            report.AppendLine(exception != null
                ? exception.Message
                : "No hay información adicional.");
            report.AppendLine();

            if (exception != null)
            {
                report.AppendLine("Tipo de excepción:");
                report.AppendLine(exception.GetType().Name);
                report.AppendLine();

                report.AppendLine("Detalles técnicos:");
                report.AppendLine(exception.StackTrace);
            }

            // Escribir el archivo de texto.
            File.WriteAllText(
                filePath,
                report.ToString(),
                Encoding.UTF8
            );

            Debug.Log("Informe de error guardado: " + filePath);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(
                "No se ha podido guardar el informe: " + e.Message
            );

            return false;
        }


    }
}
