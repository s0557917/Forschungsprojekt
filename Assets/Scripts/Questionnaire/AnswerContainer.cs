using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VrPassing.Questionnaires
{

    public class AnswerContainer : MonoBehaviour
    {
        public string questionnaireTitle;
        public string question;
        public string answer;

        private List<Button> buttons;
        private QuestionnaireManager questionnaireManager;

        void Start()
        {
            buttons = this.GetComponentsInChildren<Button>().ToList();
            buttons.ForEach(button => button.onClick.AddListener(() => SaveAnswer(button)));
            questionnaireManager = GameObject.FindObjectOfType<QuestionnaireManager>();
        }

        private void SaveAnswer(Button currentButton)
        {
            answer = currentButton.GetComponentInChildren<TextMeshProUGUI>().text;
            buttons.ForEach(button => button.interactable = false);

        }

        private void OnDestroy()
        {
            buttons.ForEach(button => button.onClick.RemoveAllListeners());
        }
    }
}
