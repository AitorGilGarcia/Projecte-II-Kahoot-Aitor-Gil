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
			title = "Mundo de los videojuegos",
			description = "Cuestionario sobre videojuegos, personajes y consolas",

			questions = new List<QuestionData>
			{
				new QuestionData
				{
					questionText = "¿Qué empresa creó la saga Super Mario?",
					answers = new string[]
					{
						"Sega",
						"Nintendo",
						"Sony",
						"Microsoft"
					},
					correctAnswerIndex = 1,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿Cómo se llama el protagonista de The Legend of Zelda?",
					answers = new string[]
					{
						"Zelda",
						"Ganondorf",
						"Link",
						"Kirby"
					},
					correctAnswerIndex = 2,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿En qué videojuego podemos construir con bloques y explorar mundos?",
					answers = new string[]
					{
						"Minecraft",
						"Fortnite",
						"Rocket League",
						"Overwatch"
					},
					correctAnswerIndex = 0,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿Cuál es la mascota más conocida de Sega?",
					answers = new string[]
					{
						"Crash Bandicoot",
						"Pikachu",
						"Sonic",
						"Mario"
					},
					correctAnswerIndex = 2,
					timeLimit = 15
				},

				new QuestionData
				{
					questionText = "¿Qué tipo de juego es FIFA (actualmente EA SPORTS FC)?",
					answers = new string[]
					{
						"Carreras",
						"Fútbol",
						"Aventura",
						"Estrategia"
					},
					correctAnswerIndex = 1,
					timeLimit = 15
				},

				new QuestionData
				{
					questionText = "¿Qué personaje de Pokémon es conocido por ser de tipo eléctrico?",
					answers = new string[]
					{
						"Charmander",
						"Bulbasaur",
						"Squirtle",
						"Pikachu"
					},
					correctAnswerIndex = 3,
					timeLimit = 15
				},

				new QuestionData
				{
					questionText = "¿En qué juego aparece el personaje Kratos?",
					answers = new string[]
					{
						"God of War",
						"The Last of Us",
						"Uncharted",
						"Horizon"
					},
					correctAnswerIndex = 0,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿Qué compañía fabrica la consola PlayStation?",
					answers = new string[]
					{
						"Nintendo",
						"Microsoft",
						"Sony",
						"Sega"
					},
					correctAnswerIndex = 2,
					timeLimit = 15
				},

				new QuestionData
				{
					questionText = "¿Cuál de estos juegos es un Battle Royale?",
					answers = new string[]
					{
						"The Sims 4",
						"Fortnite",
						"Minecraft",
						"Stardew Valley"
					},
					correctAnswerIndex = 1,
					timeLimit = 20
				},

				new QuestionData
				{
					questionText = "¿Qué significa normalmente la sigla NPC en los videojuegos?",
					answers = new string[]
					{
						"Nuevo Personaje Central",
						"Nivel Principal de Combate",
						"Personaje No Jugable",
						"Nueva Partida Completa"
					},
					correctAnswerIndex = 2,
					timeLimit = 20
				}
			}
		};
		// Enviamos los datos al creador.
		kahootCreator.LoadDefaultKahoot(defaultKahoot);
	}
}