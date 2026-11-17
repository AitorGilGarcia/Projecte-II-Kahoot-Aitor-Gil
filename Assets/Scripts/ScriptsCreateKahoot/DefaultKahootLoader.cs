using UnityEngine;
using System.Collections.Generic;


// este codigo es para crear un kahoot default
public class DefaultKahootLoader : MonoBehaviour
{
	[SerializeField] private KahootCreator kahootCreator;

	public void LoadDefaultKahoot()
	{
		// Creamos el Kahoot por defecto.
		KahootData defaultKahoot = new KahootData
		{
			title = "Sistema digestivo",
			description = "Cuestionario sobre el sistema digestivo",

			questions = new List<QuestionData>
			{
				new QuestionData
				{
					questionText = "¿Qué órgano se encarga de bombear la sangre?",
					answers = new string[]
					{
						"Estómago",
						"Corazón",
						"Hígado",
						"Intestino"
					},
					correctAnswerIndex = 1,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿Dónde comienza la digestión?",
					answers = new string[]
					{
						"Estómago",
						"Intestino delgado",
						"Boca",
						"Esófago"
					},
					correctAnswerIndex = 2,
					timeLimit = 15
				},

				new QuestionData
				{
					questionText = "¿Qué órgano produce la bilis?",
					answers = new string[]
					{
						"Páncreas",
						"Hígado",
						"Estómago",
						"Riñón"
					},
					correctAnswerIndex = 1,
					timeLimit = 20
				}
			}
		};

		// Enviamos los datos al creador.
		kahootCreator.LoadDefaultKahoot(defaultKahoot);
	}
}