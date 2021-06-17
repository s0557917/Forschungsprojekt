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
        public Questionnaire.ScaleType scaleType;

        private List<Button> buttons;
        private QuestionnaireManager questionnaireManager;
        [SerializeField] private Slider sliderScale;

        void Start()
        {
            if (scaleType == Questionnaire.ScaleType.Slider)
            {
                sliderScale?.onValueChanged.AddListener(delegate { SaveSliderValue(); });
            }

            buttons = this.GetComponentsInChildren<Button>().ToList();
            buttons.ForEach(button => button.onClick.AddListener(() => SaveButtonValue(button)));
            questionnaireManager = GameObject.FindObjectOfType<QuestionnaireManager>();
        }

        private void SaveButtonValue(Button currentButton)
        {
            if (scaleType == Questionnaire.ScaleType.IntegerPartitions)
            {
                answer = currentButton.GetComponentInChildren<TextMeshProUGUI>().text;
                buttons.ForEach(button => button.interactable = false);
            }
        }

        private void SaveSliderValue()
        {
            if (scaleType == Questionnaire.ScaleType.Slider)
            {
                answer = sliderScale.value.ToString();
            }
        }

        private void OnDestroy()
        {
            if (buttons != null && buttons.Count > 1)
            {
                buttons.ForEach(button => button.onClick.RemoveAllListeners());
            }
        }
    }
}
