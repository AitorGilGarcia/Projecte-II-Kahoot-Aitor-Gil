using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//muestra el nombre del archivo y permite seleccionarlo.
public class ReportItemUI : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TMP_Text nombreInforme;
    [SerializeField] private TMP_Text fechaInforme;

    [Header("Botón")]
    [SerializeField] private Button selectButton;

    public void Setup( string fileName, string date, Action<string> onSelected)
    {
        nombreInforme.text = fileName;
        fechaInforme.text = date;

        selectButton.onClick.RemoveAllListeners();

        selectButton.onClick.AddListener(
            () => onSelected?.Invoke(fileName)
        );
    }
}