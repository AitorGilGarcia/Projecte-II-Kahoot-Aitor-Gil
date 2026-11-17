
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KahootItemLeaderboard : MonoBehaviour
{
	[SerializeField] private TMP_Text nombreKahoot;
	[SerializeField] private TMP_Text descripcionKahoot;
	[SerializeField] private Button botonSeleccionar;

	public void Setup(KahootData kahoot, Action<KahootData> onSelected)
	{
		nombreKahoot.text = kahoot.title;
		descripcionKahoot.text = kahoot.description;

		botonSeleccionar.onClick.RemoveAllListeners();

		botonSeleccionar.onClick.AddListener(() =>
		{
			onSelected?.Invoke(kahoot);
		});
	}
}
