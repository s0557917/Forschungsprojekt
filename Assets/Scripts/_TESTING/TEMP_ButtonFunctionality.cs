using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

public class TEMP_ButtonFunctionality : MonoBehaviour
{
    public Button buttonOne;
    public Button buttonTwo;

    public UnityEvent generateQuestionnaireUI;
    public UnityEvent destroyQuestionnaireUI;

    void Start()
    {
        buttonOne.onClick.AddListener(() => ButtonOneFunctionality()); 
        buttonTwo.onClick.AddListener(() => ButtonTwoFunctionality()); 
    }

    private void ButtonOneFunctionality()
    {
        Debug.Log("###### -- BUTON 1 WAS CLICKED!!");
        generateQuestionnaireUI.Invoke();
    }

    private void ButtonTwoFunctionality()
    {
        Debug.Log("###### -- BUTON 2 WAS CLICKED!!");
        generateQuestionnaireUI.Invoke();
    }
}
