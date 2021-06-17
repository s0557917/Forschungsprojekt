using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace VrPassing.Questionnaires
{
    public class QuestionnaireUIGenerator : MonoBehaviour
    {
        public GameObject questionnaireCanvas;

        [SerializeField]
        private GameObject questionnaireUIPrefab;
        [SerializeField]
        private GameObject questionContainerPrefab;
        [SerializeField]
        private GameObject questionButtonPrefab;
        [SerializeField]
        private GameObject questionSliderPrefab;
        [SerializeField]
        private GameObject partitionButtonPrefab;
        [SerializeField]
        private GameObject sliderScalaPrefab;

        private int questionsPerPage;
        private List<AnswerContainer> answerContainers = new List<AnswerContainer>();

        void Awake()
        {
            questionnaireCanvas = this.gameObject;
        }

        public void DestroyQuestionnaires()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public List<QuestionnaireUIContainer> GenerateQuestionnaireUI(List<Questionnaire> questionnaires, int questionsPerPage)
        {
            this.questionsPerPage = questionsPerPage;
            List<QuestionnaireUIContainer> questionnaireUIs = new List<QuestionnaireUIContainer>();

            foreach (Questionnaire questionnaire in questionnaires)
            {
                GameObject questionnaireUIInstance = GameObject.Instantiate(questionnaireUIPrefab, questionnaireCanvas.transform);
                questionnaireUIInstance.name = questionnaire.title;
                questionnaireUIInstance.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = questionnaire.title;

                QuestionnairePage[] questionnairePages = GeneratePages(questionnaire.questions, questionnaire.title, questionnaireUIInstance.transform, questionnaire.scaleStart, questionnaire.scaleEnd, questionnaire.partitionCount, questionnaire.scaleType);
                QuestionnaireUIContainer questionnaireData = new QuestionnaireUIContainer(questionnaireUIInstance, questionnairePages, answerContainers);
                questionnaireUIs.Add(questionnaireData);
            }

            return questionnaireUIs;
        }

        private QuestionnairePage[] GeneratePages(List<QuestionnaireQuestion> questions, string questionnaireTitle, Transform questionnaireUIInstance, float scaleStart, float scaleEnd, int partitions, Questionnaire.ScaleType scaleType)
        {
            int pageCount = (int)Math.Ceiling((double)questions.Count / questionsPerPage);
            GameObject[] pageUIs = new GameObject[pageCount];

            for (int i = 0; i < pageCount; i++)
            {
                pageUIs[i] = GameObject.Instantiate(questionContainerPrefab, questionnaireUIInstance);
                pageUIs[i].name = "Page " + i;

                if (i != 0)
                {
                    pageUIs[i].SetActive(false);
                }
            }

            QuestionnairePage[] questionnairePages = new QuestionnairePage[pageCount];

            for (int i = 0; i < pageCount; i++)
            {
                QuestionnairePage page;

                if (i == pageCount - 1)
                {
                    page = new QuestionnairePage(pageUIs[i], null, pageUIs[i].transform.GetChild(1).GetComponent<Button>());
                }
                else
                {
                    page = new QuestionnairePage(pageUIs[i], pageUIs[i + 1], pageUIs[i].transform.GetChild(1).GetComponent<Button>());
                }

                questionnairePages[i] = page;

            }

            int pageCounter = 0;
            for (int i = 0; i <= questions.Count; i += questionsPerPage)
            {
                for (int j = i; j < i + questionsPerPage; j++)
                {
                    try
                    {
                        GameObject scaleContainer = AddQuestionsToPage(questions[j], pageUIs[pageCounter].transform, questionnaireTitle, scaleType);
                        if (scaleType == Questionnaire.ScaleType.IntegerPartitions)
                        {
                            SetupLayoutGroup(scaleContainer);
                            AddButtons(scaleStart, scaleEnd, partitions, scaleContainer.transform);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                pageCounter++;
            }

            return questionnairePages;
        }

        private void SetupLayoutGroup(GameObject scaleContainer)
        {
            HorizontalLayoutGroup layoutGroup = scaleContainer.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childScaleWidth = false;
            layoutGroup.childScaleHeight = false;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;
        }

        private GameObject AddQuestionsToPage(QuestionnaireQuestion question, Transform questionContainer, string questionnaireTitle, Questionnaire.ScaleType scaleType)
        {
            GameObject questionInstance = null;

            switch (scaleType)
            {
                case Questionnaire.ScaleType.IntegerPartitions:
                    questionInstance = GameObject.Instantiate(questionButtonPrefab, questionContainer.GetChild(0).transform);
                    break;
                case Questionnaire.ScaleType.Slider:
                    questionInstance = GameObject.Instantiate(questionSliderPrefab, questionContainer.GetChild(0).transform);
                    break;
                default:
                    Debug.Log("No valid scale type!");
                    break;
            }

            questionInstance.name = question.question;
            questionInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.question;
            questionInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.leftText;
            questionInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = question.rightText;

            GameObject scaleContainer = questionInstance.transform.GetChild(3).gameObject;
            AnswerContainer answerContainer = scaleContainer.GetComponent<AnswerContainer>();
            answerContainer.questionnaireTitle = questionnaireTitle;
            answerContainer.question = question.question;
            answerContainer.scaleType = scaleType;
            answerContainers.Add(answerContainer);

            return scaleContainer;
        }

        private void AddButtons(float scaleStart, float scaleEnd, int partitions, Transform scaleContainer)
        {
            float scaleIncrementSize = (scaleEnd - scaleStart) / partitions;
            float currentPartitionValue = scaleStart;

            for (int i = 0; i < partitions; i++)
            {
                GameObject instantiatedButton = Instantiate(partitionButtonPrefab, scaleContainer);
                instantiatedButton.GetComponentInChildren<TextMeshProUGUI>().text = currentPartitionValue.ToString();
                currentPartitionValue += scaleIncrementSize;
            }
        }
    }
}

