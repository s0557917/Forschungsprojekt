using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace VrPassing.Questionnaires
{
    public class AnswerTracker : MonoBehaviour
    {
        private AnswerContainer[] answerContainers;
        private int pageQuestionsCount;
        [SerializeField]
        private Button paginationButton;

        IEnumerator disablePaginationCoroutine;

        private void Start()
        {
            pageQuestionsCount = this.transform.childCount;
            answerContainers = new AnswerContainer[pageQuestionsCount];

            for (int i = 0; i < pageQuestionsCount; i++)
            {
                answerContainers[i] = this.transform.GetChild(i).GetComponentInChildren<AnswerContainer>();
            }

            paginationButton = this.transform.parent.GetChild(1).GetComponent<Button>();
            paginationButton.interactable = false;

            //if (disablePaginationCoroutine != null)
            //{
            //    StopCoroutine(disablePaginationCoroutine);
            //    disablePaginationCoroutine = null;
            //}
            //else
            //{
            //    disablePaginationCoroutine = DisablePaginationUntilAnswered();
            //    StartCoroutine(disablePaginationCoroutine);
            //}
        }

        private IEnumerator DisablePaginationUntilAnswered()
        {
            while (answerContainers.ToList().Any(container => string.IsNullOrEmpty(container.answer)))
            {
                yield return null;
            }

            paginationButton.interactable = true;

        }

        private void OnDestroy()
        {
            if (disablePaginationCoroutine != null)
            {
                StopCoroutine(disablePaginationCoroutine);
            }
        }

        private void OnDisable()
        {
            if (disablePaginationCoroutine != null)
            {
                StopCoroutine(disablePaginationCoroutine);
            }
        }
    }
}

