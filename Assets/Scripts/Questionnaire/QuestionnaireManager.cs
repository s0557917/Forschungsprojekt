using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;
using VrPassing.Helpers;

namespace VrPassing.Questionnaires
{
    public class QuestionnaireManager : MonoBehaviour
    {
        [SerializeField] private int questionsPerPage = 2;
        [SerializeField] private List<Questionnaire> questionnaires = new List<Questionnaire>();
        [SerializeField] private List<QuestionnaireUIContainer> generatedQuestionnaires;
        private QuestionnaireUIGenerator questionnaireGenerator;
        
        void Start()
        {
            XRSettings.enabled = true;
            questionnaireGenerator = this.GetComponent<QuestionnaireUIGenerator>();

            GenerateQuestionnaire();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateQuestionnaire();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                questionnaireGenerator.DestroyQuestionnaires();
            }
        }

        private void GenerateQuestionnaire()
        {
            generatedQuestionnaires = questionnaireGenerator.GenerateQuestionnaireUI(questionnaires, questionsPerPage);

            for (int i = 1; i < generatedQuestionnaires.Count; i++)
            {
                generatedQuestionnaires[i].questionnaireUI.SetActive(false);
            }

            AddPaginationFunctionality(this.gameObject);
        }

        private void AddPaginationFunctionality(GameObject questionnaireManager)
        {
            for (int i = 0; i < generatedQuestionnaires.Count; i++)
            {
                for (int j = 0; j < generatedQuestionnaires[i].questionnairePages.Length; j++)
                {
                    int questionnaireIter = i;
                    int totalQuestionnaireAmount = generatedQuestionnaires.Count;

                    Button paginationButton = generatedQuestionnaires[i].questionnairePages[j].paginationButton;
                    GameObject currentPageUI = generatedQuestionnaires[i].questionnairePages[j].pageUI;
                    GameObject nextPageUI = generatedQuestionnaires[i].questionnairePages[j].nextPageUI;
                    GameObject currentQuestionnaire = generatedQuestionnaires[i].questionnaireUI;
                    GameObject questionnaireManagerInstance = questionnaireManager;
                    GameObject nextQuestionnaire;

                    if (i + 1 >= generatedQuestionnaires.Count)
                    {
                        nextQuestionnaire = null;
                    }
                    else
                    {
                        nextQuestionnaire = generatedQuestionnaires[i + 1].questionnaireUI;
                    }

                    paginationButton.onClick.AddListener(() =>
                    {

                        if (nextQuestionnaire == null)
                        {
                            questionnaireManagerInstance.SetActive(false);
                            SaveAnswersToFile(generatedQuestionnaires[questionnaireIter].singleAnswerContainers);
                        }
                        else if (nextPageUI != null)
                        {
                            nextPageUI.SetActive(true);
                            currentPageUI.SetActive(false);
                        }
                        else if (nextPageUI == null)
                        {
                            if (questionnaireIter < totalQuestionnaireAmount - 1)
                            {
                                nextQuestionnaire.SetActive(true);
                                currentQuestionnaire.SetActive(false);
                            }

                        }
                    });
                }
            }
        }

        private void SaveAnswersToFile(List<AnswerContainer> answerContainers)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("--------- Questionnaire Answers ---------");

            foreach (AnswerContainer answerContainer in answerContainers)
            {

                stringBuilder.AppendLine("-- " + answerContainer.questionnaireTitle
                                        + " - Question: " + answerContainer.question
                                        + " - Answer: " + answerContainer.answer);
            }

            stringBuilder.AppendLine("--------- Questionnaire Answers ---------");
            string answersString = stringBuilder.ToString();

            FileWriter.WriteToFile(PlayerPrefs.GetString("SessionFilePath"), answersString);
        }
    }

}

