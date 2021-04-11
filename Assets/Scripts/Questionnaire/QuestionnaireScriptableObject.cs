using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsabilityQuestionnaire", menuName = "Usability/New Questionnaire", order = 1)]
public class Questionnaire : ScriptableObject
{
    public string questionnaireTitle;
    public List<QuestionnaireQuestions> questionnaireQuestions = new List<QuestionnaireQuestions>();
}

[CreateAssetMenu(fileName = "UsabilityQuestion", menuName = "Usability/New Question", order = 2)]
public class QuestionnaireQuestions : ScriptableObject
{
    public string question;
    public string leftText;
    public string rightText;
    public int scaleDivisions;
}

