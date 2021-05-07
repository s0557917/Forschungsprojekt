using UnityEngine;

[CreateAssetMenu(fileName = "UsabilityQuestion", menuName = "Usability/New Question", order = 2)]
public class QuestionnaireQuestion : ScriptableObject
{
    public string question;
    public string leftText;
    public string rightText;
}