using System;
using System.Collections.Generic;

[Serializable]
public class KahootData // este script almacena los datos generales del Kahoot
{
    public string title;
    public string description;

    public List<QuestionData> questions = new List<QuestionData>();
}

