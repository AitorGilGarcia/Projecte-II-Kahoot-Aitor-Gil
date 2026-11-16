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

    [Header("Textos de la interfaz")]
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text questionCounterText;

    [Header("Botones de respuestas")]
    [SerializeField] private Button[] answerButtons;

    [Header("Configuración")]
    [SerializeField] private float feedbackDuration = 3f;

    private bool answered = false;
    private Coroutine questionCoroutine;

    private void Start()
    {
        currentKahoot = GameSession.SelectedKahoot;

        if (currentKahoot == null || currentKahoot.questions.Count == 0)
        {
            Debug.LogError("No hay ningún Kahoot seleccionado o no tiene preguntas.");
            return;
        }

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

        QuestionData question = currentKahoot.questions[currentQuestionIndex];

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
        int remainingTime = timeLimit > 0 ? timeLimit : 20;

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

    private void EndGame()
    {
        Debug.Log("¡Partida finalizada!");

        // Más adelante mostraremos el panel de resultados.
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