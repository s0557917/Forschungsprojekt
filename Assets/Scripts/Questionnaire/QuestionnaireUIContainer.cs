using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VrPassing.Questionnaires
{
    [System.Serializable]
    public class QuestionnaireUIContainer
    {
        [SerializeField]
        public GameObject questionnaireUI;
        [SerializeField]
        public QuestionnairePage[] questionnairePages;
        [SerializeField]
        public List<AnswerContainer> singleAnswerContainers;

        public QuestionnaireUIContainer(GameObject questionnaireUI,
                                        QuestionnairePage[] questionnairePaiges,
                                        List<AnswerContainer> singleAnswerContainers)
        {
            this.questionnaireUI = questionnaireUI;
            this.questionnairePages = questionnairePaiges;
            this.singleAnswerContainers = singleAnswerContainers;
        }
    }

    [System.Serializable]
    public class QuestionnairePage
    {
        [SerializeField]
        public GameObject pageUI;
        [SerializeField]
        public GameObject nextPageUI;
        [SerializeField]
        public Button paginationButton;

        public QuestionnairePage(GameObject pageUI, GameObject nextPageUI, Button paginationButton)
        {
            this.pageUI = pageUI;
            this.nextPageUI = nextPageUI;
            this.paginationButton = paginationButton;
        }
    }
}
