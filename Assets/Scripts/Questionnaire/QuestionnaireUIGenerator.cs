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
        private GameObject questionnaireCanvas;

        [SerializeField]
        private GameObject questionnaireUIPrefab;
        [SerializeField]
        private GameObject questionContainerPrefab;
        [SerializeField]
        private GameObject questionPrefab;
        [SerializeField]
        private GameObject partitionButtonPrefab;
        private int questionsPerPage;
        private List<AnswerContainer> answerContainers = new List<AnswerContainer>();

        void Start()
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

        public List<QuestionnaireUIContainer> GenerateQuestionnaire(List<Questionnaire> questionnaires, int questionsPerPage)
        {
            this.questionsPerPage = questionsPerPage;
            List<QuestionnaireUIContainer> questionnaireUIs = new List<QuestionnaireUIContainer>();

            foreach (Questionnaire questionnaire in questionnaires)
            {
                GameObject questionnaireUIInstance = GameObject.Instantiate(questionnaireUIPrefab, questionnaireCanvas.transform);
                questionnaireUIInstance.name = questionnaire.title;
                questionnaireUIInstance.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = questionnaire.title;

                QuestionnairePage[] questionnairePages = GeneratePages(questionnaire.questions, questionnaire.title, questionnaireUIInstance.transform, questionnaire.partitionCount);
                QuestionnaireUIContainer questionnaireData = new QuestionnaireUIContainer(questionnaireUIInstance, questionnairePages, answerContainers);
                questionnaireUIs.Add(questionnaireData);
            }

            return questionnaireUIs;
        }

        private QuestionnairePage[] GeneratePages(List<QuestionnaireQuestion> questions, string questionnaireTitle, Transform questionnaireUIInstance, int partitions)
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
                        GameObject firstPartitionContainer = AddQuestionsToPage(questions[j], pageUIs[pageCounter].transform, questionnaireTitle);
                        AddButtons(partitions, firstPartitionContainer.transform);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("NO QUESTION HERE -- NEEDS TO BE FIXED!!");
                    }
                }

                pageCounter++;
            }

            return questionnairePages;
        }

        private GameObject AddQuestionsToPage(QuestionnaireQuestion question, Transform questionContainer, string questionnaireTitle)
        {
            GameObject questionInstance = GameObject.Instantiate(questionPrefab, questionContainer.GetChild(0).transform);
            questionInstance.name = question.question;
            questionInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.question;
            questionInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = question.leftText;
            questionInstance.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = question.rightText;

            GameObject partitionContainer = questionInstance.transform.GetChild(3).gameObject;
            AnswerContainer answerContainer = partitionContainer.GetComponent<AnswerContainer>();
            answerContainer.questionnaireTitle = questionnaireTitle;
            answerContainer.question = question.question;
            answerContainers.Add(answerContainer);
            return partitionContainer;
        }

        private void AddButtons(int partitions, Transform partitionContainer)
        {
            for (int i = 0; i < partitions; i++)
            {
                GameObject instantiatedButton = GameObject.Instantiate(partitionButtonPrefab, partitionContainer);
                instantiatedButton.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            }
        }
    }
}

