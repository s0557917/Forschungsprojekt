using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UsabilityQuestionnaire", menuName = "Usability/New Questionnaire", order = 1)]
public class Questionnaire : ScriptableObject
{
    public string title;
    public List<QuestionnaireQuestion> questions = new List<QuestionnaireQuestion>();
    public int partitionCount;
}



