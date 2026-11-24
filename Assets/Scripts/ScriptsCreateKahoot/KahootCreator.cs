
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
	[SerializeField] private UnityEngine.UI.Button[] correctAnswerButtons;

	// Todas las preguntas del Kahoot.
	private List<QuestionData> questions = new List<QuestionData>(); //utiliza QuestionData

    // Índice de la pregunta que estamos editando.
    private int currentQuestionIndex = 0;

    private void Start()
    {
        // Limitamos el número de caracteres de cada campo
        titleInput.characterLimit = 30;
        descriptionInput.characterLimit = 50;

        timeInput.characterLimit = 2;
        questionInput.characterLimit = 90;

        foreach (TMP_InputField answer in answerInputs)
        {
            answer.characterLimit = 80;
        }

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

        timeInput.text = question.timeLimit > 0 ? question.timeLimit.ToString() : "20";

		UpdateCorrectAnswerColor();
	}

    // Selecciona la respuesta correcta.
    public void SetCorrectAnswer(int answerIndex)
    {
        if (answerIndex < 0 || answerIndex >= 4)
            return;

        questions[currentQuestionIndex].correctAnswerIndex = answerIndex;
		UpdateCorrectAnswerColor();
	}

	// Actualiza el color de los botones segun la respuesta seleccionada
	private void UpdateCorrectAnswerColor()
	{
		int correctIndex = questions[currentQuestionIndex].correctAnswerIndex;

		for (int i = 0; i < correctAnswerButtons.Length; i++)
		{
			if (correctAnswerButtons[i] == null) {
				continue;
			}

			correctAnswerButtons[i].image.color = i == correctIndex ? Color.green : Color.white;
		}
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


	// Comprueba si una pregunta está completamente vacía.
	private bool IsQuestionEmpty(QuestionData question)
	{
		if (!string.IsNullOrWhiteSpace(question.questionText))
			return false;

		foreach (string answer in question.answers)
		{
			if (!string.IsNullOrWhiteSpace(answer))
				return false;
		}

		return true;
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

		// Creamos una lista solamente con las preguntas que tienen contenido.
		List<QuestionData> validQuestions = questions.FindAll(question => !IsQuestionEmpty(question));

		// Comprobamos que haya al menos una pregunta.
		if (validQuestions.Count == 0)
		{
			Debug.LogWarning("El Kahoot necesita al menos una pregunta.");
			return;
		}


		// Creamos el objeto con todos los datos.
		KahootData kahoot = new KahootData
        {
            title = titleInput.text,
            description = descriptionInput.text,
            questions = validQuestions // metemos las respuesas validad y no las vacias
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

