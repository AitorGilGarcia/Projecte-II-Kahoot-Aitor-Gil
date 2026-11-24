
using UnityEngine;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

public static class LeaderboardSaveSystem
{
    // Devuelve la ruta del XML correspondiente a un Kahoot.
    private static string GetFilePath(KahootData kahoot)
    {
        string folderPath = Path.Combine(Application.persistentDataPath,"Leaderboards");

        Directory.CreateDirectory(folderPath);

        return Path.Combine(folderPath, kahoot.id + ".xml");
    }

    // Guarda un resultado de una partida.
    public static bool SaveResult( KahootData kahoot, string playerName, int score, int totalTime)
    {
        try
        {
            string filePath = GetFilePath(kahoot);

            LeaderboardData leaderboard = LoadResults(kahoot);

            GameResult newResult = new GameResult
            {
                playerName = playerName,
                score = score,
                totalTime = totalTime
            };

            // Buscamos si el jugador ya tiene un resultado guardado.
            GameResult previousResult = leaderboard.results
                .FirstOrDefault(r =>
                    r.playerName.Equals(
                        playerName,
                        StringComparison.OrdinalIgnoreCase
                    ));

            if (previousResult != null)
            {
                // Conservamos su mejor puntuación.
                if (score > previousResult.score ||
                    (score == previousResult.score &&
                     totalTime < previousResult.totalTime))
                {
                    previousResult.score = score;
                    previousResult.totalTime = totalTime;
                }
            }
            else
            {
                leaderboard.results.Add(newResult);
            }

            // Ordenamos por puntuación y después por tiempo.
            leaderboard.results = leaderboard.results
                .OrderByDescending(r => r.score)
                .ThenBy(r => r.totalTime)
                .ToList();

            XmlSerializer serializer = new XmlSerializer(typeof(LeaderboardData));

            using (FileStream stream = new FileStream(
                filePath, FileMode.Create))
            {
                serializer.Serialize(stream, leaderboard);
            }

            Debug.Log("Leaderboard guardado: " + filePath);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error guardando leaderboard: " + e.Message);
            return false;
        }
    }

    // Lee los resultados de un Kahoot.
    public static LeaderboardData LoadResults(KahootData kahoot)
    {
        string filePath = GetFilePath(kahoot);

        // Si todavía no hay XML, devolvemos una lista vacía.
        if (!File.Exists(filePath))
        {
            return new LeaderboardData();
        }

        try
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(LeaderboardData));

            using (FileStream stream = new FileStream(
                filePath, FileMode.Open))
            {
                return (LeaderboardData)serializer.Deserialize(stream);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error leyendo leaderboard: " + e.Message);
            return new LeaderboardData();
        }
    }
}
