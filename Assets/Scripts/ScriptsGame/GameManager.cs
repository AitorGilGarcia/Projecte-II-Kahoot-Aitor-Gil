using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Datos de la partida")]
    private KahootData currentKahoot;
    private int currentQuestionIndex = 0;

    [Header("Puntuación")]
    private int totalScore = 0;
    [SerializeField] private int maxPoints = 1000;

    [Header("Textos de la interfaz")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text questionCounterText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreFinalText;

    [Header("Botones de respuestas")]
    [SerializeField] private Button[] answerButtons;

    [Header("Configuración")]
    [SerializeField] private float feedbackDuration = 3f;

    [Header("Paneles")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject resultsPanel;


    private float gameStartTime;
    private bool answered = false;
    private Coroutine questionCoroutine;
    private int remainingTime; // variable que es para saber cuanto le queda de tiempo, mas enfocado para la puntuación
    private void Start()
    {

        scoreText.text = "Puntos: 0"; // setteo la puntuación a 0 en el texto 

        currentKahoot = GameSession.SelectedKahoot;

        if (currentKahoot == null || currentKahoot.questions.Count == 0)
        {
            Debug.LogError("No hay ningún Kahoot seleccionado o no tiene preguntas.");
            return;
        }

        gameStartTime = Time.time;
        // Conectamos los cuatro botones con sus respectivos índices.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int answerIndex = i;

            answerButtons[i].onClick.AddListener(() =>
            {
                SelectAnswer(answerIndex);
            });
        }

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        answered = false;

        QuestionData question = currentKahoot.questions[currentQuestionIndex]; // pilla la pregunta del currentKahoot, que es el GameSession

        // Mostramos el enunciado.
        questionText.text = question.questionText;

        // Mostramos el número de pregunta.
        questionCounterText.text =
            "Pregunta " + (currentQuestionIndex + 1) +
            " / " + currentKahoot.questions.Count;

        // Mostramos las cuatro respuestas.
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TMP_Text answerText =
                answerButtons[i].GetComponentInChildren<TMP_Text>();

            answerText.text = question.answers[i];

            // Recuperamos el color original.
            answerButtons[i].targetGraphic.color = Color.white;

            // Permitimos volver a pulsar el botón.
            answerButtons[i].interactable = true;
        }

        // Iniciamos el temporizador de esta pregunta.
        questionCoroutine = StartCoroutine(QuestionTimer(question.timeLimit));
    }

    private IEnumerator QuestionTimer(int timeLimit)
    {
        remainingTime = timeLimit > 0 ? timeLimit : 20;

        while (remainingTime > 0 && !answered)
        {
            timerText.text = remainingTime.ToString();

            yield return new WaitForSeconds(1f);

            remainingTime--;
        }

        // Si se acaba el tiempo sin responder.
        if (!answered)
        {
            answered = true;

            RevealAnswer(-1);

            yield return new WaitForSeconds(feedbackDuration);

            NextQuestion();
        }
    }

    private void SelectAnswer(int answerIndex)
    {
        // Evitamos que se pueda responder varias veces.
        if (answered)
            return;

        answered = true;

        // Detenemos la cuenta atrás.
        if (questionCoroutine != null)
            StopCoroutine(questionCoroutine);

        QuestionData question =
        currentKahoot.questions[currentQuestionIndex];

        if (answerIndex == question.correctAnswerIndex)
        {
            CalculateScore();
        }


        // Mostramos los colores de las respuestas.
        RevealAnswer(answerIndex);

        // Esperamos unos segundos y pasamos a la siguiente.
        questionCoroutine = StartCoroutine(WaitAndContinue());
    }

    private void RevealAnswer(int selectedIndex)
    {
        QuestionData question =
            currentKahoot.questions[currentQuestionIndex];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Desactivamos todos los botones.
            answerButtons[i].interactable = false;

            // La respuesta correcta siempre aparece en verde.
            if (i == question.correctAnswerIndex)
            {
                answerButtons[i].targetGraphic.color = Color.green;
            }
            else
            {
                // Las respuestas incorrectas aparecen en rojo.
                answerButtons[i].targetGraphic.color = Color.red;
            }
        }

        if (selectedIndex == -1)
        {
            Debug.Log("Se ha acabado el tiempo.");
        }
        else if (selectedIndex == question.correctAnswerIndex)
        {
            Debug.Log("¡Respuesta correcta!");
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
        }
    }

    private IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(feedbackDuration);

        NextQuestion();
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;

        // Comprobamos si quedan preguntas.
        if (currentQuestionIndex < currentKahoot.questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            EndGame();
        }
    }
    private void CalculateScore()
    {
        QuestionData question =
            currentKahoot.questions[currentQuestionIndex];

        int timeLimit = question.timeLimit > 0
            ? question.timeLimit
            : 20;

        int points = Mathf.RoundToInt(
            maxPoints * (float)remainingTime / timeLimit
        );

        totalScore += points;
         
        scoreText.text = "Puntos: " + totalScore; // actualizo el texto con sus puntos que tiene 
         
        Debug.Log("Puntos conseguidos: " + points);
        Debug.Log("Puntuación total: " + totalScore);
    }

    private void EndGame()
    {
        //Debug.Log("¡Partida finalizada!");

        //scoreFinalText.text = "Puntuación final: " + totalScore; // actualizamos el texto final para mostrar la puntuación

        //gamePanel.SetActive(false);
        //resultsPanel.SetActive(true);

        Debug.Log("¡Partida finalizada!");

        scoreFinalText.text = "Puntuación final: " + totalScore;

        // Calculamos el tiempo total de la partida.
        int totalTime = Mathf.RoundToInt(Time.time - gameStartTime);

        // Recuperamos el nombre del jugador.
        string playerName = PlayerData.PlayerName;

        // Guardamos el resultado.
        LeaderboardSaveSystem.SaveResult(currentKahoot, playerName, totalScore, totalTime);

        gamePanel.SetActive(false);
        resultsPanel.SetActive(true);



    }
}

//este script lo que hace es gestionar la partida


// test para ver si se conecta al kahoot seleccionado 

//private void Start()
//{
//    currentKahoot = GameSession.SelectedKahoot;

//    if (currentKahoot == null)
//    {
//        Debug.LogError("No se ha seleccionado ningún Kahoot.");
//        return;
//    }

//    Debug.Log("Kahoot cargado: " + currentKahoot.title);
//    Debug.Log("Número de preguntas: " + currentKahoot.questions.Count);
//}