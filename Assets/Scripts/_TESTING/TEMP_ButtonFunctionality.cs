using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMP_ButtonFunctionality : MonoBehaviour
{
    public Button buttonOne;
    public Button buttonTwo;

    void Start()
    {
        buttonOne.onClick.AddListener(() => ButtonOneFunctionality()); 
        buttonTwo.onClick.AddListener(() => ButtonTwoFunctionality()); 
    }

    private void ButtonOneFunctionality()
    {
        Debug.Log("###### -- BUTON 1 WAS CLICKED!!");
    }

    private void ButtonTwoFunctionality()
    {
        Debug.Log("###### -- BUTON 2 WAS CLICKED!!");
    }
}
