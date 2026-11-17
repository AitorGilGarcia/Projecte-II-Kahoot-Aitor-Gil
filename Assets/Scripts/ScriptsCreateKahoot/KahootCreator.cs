
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KahootCreator : MonoBehaviour
// este script gestiona la interfaz, las preguntas y la navegación

{
    [Header("Datos generales")]
    [SerializeField] private TMP_InputField titleInput;
    [SerializeField] private TMP_InputField descriptionInput;

    [Header("Datos de la pregunta")]
    [SerializeField] private TMP_InputField timeInput;
    [SerializeField] private TMP_InputField questionInput;
    [SerializeField] private TMP_InputField[] answerInputs;

    // Todas las preguntas del Kahoot.
    private List<QuestionData> questions = new List<QuestionData>(); //utiliza QuestionData

    // Índice de la pregunta que estamos editando.
    private int currentQuestionIndex = 0;

    private void Start()
    {
        // Empezamos con una pregunta vacía.
        questions.Add(new QuestionData());

        LoadQuestion();
    }

    // Guarda el contenido actual en la lista.
    private void SaveCurrentQuestion()
    {
        QuestionData question = questions[currentQuestionIndex];

        question.questionText = questionInput.text;

        for (int i = 0; i < answerInputs.Length; i++)
        {
            question.answers[i] = answerInputs[i].text;
        }

        question.timeLimit = int.TryParse(timeInput.text, out int time)
            ? time
            : 20;
    }

    // Carga los datos de la pregunta seleccionada.
    private void LoadQuestion()
    {
        QuestionData question = questions[currentQuestionIndex];

        questionInput.text = question.questionText;

        for (int i = 0; i < answerInputs.Length; i++)
        {
            answerInputs[i].text = question.answers[i];
        }

        timeInput.text = question.timeLimit > 0
            ? question.timeLimit.ToString()
            : "20";
    }

    // Selecciona la respuesta correcta.
    public void SetCorrectAnswer(int answerIndex)
    {
        if (answerIndex < 0 || answerIndex >= 4)
            return;

        questions[currentQuestionIndex].correctAnswerIndex = answerIndex;
    }

    // Va a la pregunta anterior.
    public void PreviousQuestion()
    {
        SaveCurrentQuestion();

        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            LoadQuestion();
        }
    }

    // Va a la siguiente pregunta o crea una nueva.
    public void NextQuestion()
    {
        SaveCurrentQuestion();

        // Si estamos en la última pregunta, creamos otra.
        if (currentQuestionIndex == questions.Count - 1)
        {
            questions.Add(new QuestionData());
        }

        currentQuestionIndex++;

        LoadQuestion();
    }


    // Guarda el Kahoot completo en un archivo JSON.
    public void SaveKahoot()
    {
        // Guardamos primero la pregunta que estamos editando.
        SaveCurrentQuestion();

        if (string.IsNullOrWhiteSpace(titleInput.text))
        {
            Debug.LogWarning("El Kahoot necesita un título.");
            return;
        }

        // Creamos el objeto con todos los datos.
        KahootData kahoot = new KahootData
        {
            title = titleInput.text,
            description = descriptionInput.text,
            questions = questions
        };

        // Guardamos el archivo JSON.
        KahootSaveSystem.Save(kahoot);
    }


	public void LoadDefaultKahoot(KahootData kahoot)
	{
		// Cargamos los datos generales.
		titleInput.text = kahoot.title;
		descriptionInput.text = kahoot.description;

		// Sustituimos las preguntas actuales por las del Kahoot por defecto.
		questions = new List<QuestionData>(kahoot.questions);

		// Volvemos a la primera pregunta.
		currentQuestionIndex = 0;

		// Mostramos la primera pregunta en la interfaz.
		LoadQuestion();
	}

}

