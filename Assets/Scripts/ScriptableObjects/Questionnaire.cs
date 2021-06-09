using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "UsabilityQuestionnaire", menuName = "Usability/New Questionnaire", order = 1)]
public class Questionnaire : ScriptableObject
{
    public string title;
    public float scaleStart;
    public float scaleEnd;
    public int partitionCount;
    public ScaleType scaleType;
    public List<QuestionnaireQuestion> questions = new List<QuestionnaireQuestion>();

    public enum ScaleType
    {
        IntegerPartitions,
        Slider
    }
}



