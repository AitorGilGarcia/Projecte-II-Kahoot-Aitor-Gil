
using System;
using TMPro;
using UnityEngine;

public class LeaderboardItemUI : MonoBehaviour // Este script irá en el objeto raíz del prefab LeaderboardItem
{
    [Header("Textos")]
    [SerializeField] private TMP_Text positionText;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void Setup(GameResult result, int position)
    {
        positionText.text = position + ".";
        playerNameText.text = result.playerName;
        scoreText.text = result.score + " puntos";
        timeText.text = result.totalTime + " s";
    }
}
