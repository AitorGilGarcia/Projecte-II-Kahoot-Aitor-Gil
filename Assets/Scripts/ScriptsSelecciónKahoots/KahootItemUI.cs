
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KahootItemUI : MonoBehaviour
{
    [Header("Elementos de la interfaz")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button selectButton;

    private KahootData kahootData;

    // Recibe los datos y los muestra en la interfaz.
    public void Setup(KahootData data, Action<KahootData> onSelected)
    {
        kahootData = data;

        titleText.text = data.title;
        descriptionText.text = data.description;

        // Asociamos la selección del botón con el Kahoot.
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            onSelected?.Invoke(kahootData);
        });
    }
}
