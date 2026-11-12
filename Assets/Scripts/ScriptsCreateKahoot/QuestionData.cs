
using System;

[Serializable]
public class QuestionData

// script para almacenar los datos de cada pregunta.
{
    public string questionText;

    // Las cuatro respuestas posibles.
    public string[] answers = new string[4];

    // Índice de la respuesta correcta (0, 1, 2 o 3).
    public int correctAnswerIndex;

    // Tiempo disponible para responder.
    public int timeLimit;
}
