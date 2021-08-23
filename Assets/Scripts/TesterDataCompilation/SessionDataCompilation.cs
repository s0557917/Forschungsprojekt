using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;
using VrPassing.Utilities;
using UnityEngine.SceneManagement;

public class SessionDataCompilation : MonoBehaviour
{
    [Header("Session Information UI")]
    [SerializeField] private GameObject sessionInformationUI;
    [SerializeField] private TMP_InputField sessionNameInput;
    [SerializeField] private TMP_InputField sessionFolderLocation;
    [SerializeField] private Button saveSessionInformationButton;

    [Header("Tester Information UI")]
    [SerializeField] private GameObject testerInformationUI;
    [SerializeField] private TMP_InputField testerNameInput;
    [SerializeField] private TMP_InputField testerOccupationInput;
    [SerializeField] private TMP_Dropdown ageDropdown;
    [SerializeField] private TMP_Dropdown genderDropdown;
    [SerializeField] private Toggle lowExperienceToggle;
    [SerializeField] private Toggle moderateExperienceToggle;
    [SerializeField] private Toggle highExperienceToggle;
    [SerializeField] private Button saveTesterInformationButton;

    private string sessionFolder;

    void Start()
    {
        XRSettings.enabled = false;
        saveSessionInformationButton.onClick.AddListener(() => CreateSessionFolder());
        saveTesterInformationButton.onClick.AddListener(() => WriteDemographicDataToFile());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sessionNameInput.text = "Session 1";
            sessionFolderLocation.text = @"C:\Users\Phillip Friedel\Desktop";

            testerNameInput.text = "Tester Person";
            testerOccupationInput.text = "Tester";
            ageDropdown.value = 2;
            genderDropdown.value = 2;
            lowExperienceToggle.isOn = true;
        }
    }

    private void CreateSessionFolder()
    {
        sessionInformationUI.SetActive(false);
        testerInformationUI.SetActive(true);

        sessionFolder = sessionFolderLocation.text + "/" + sessionNameInput.text;

        if (!Directory.Exists(sessionFolder))
        {
            Directory.CreateDirectory(sessionFolder);
        }

        PlayerPrefs.SetString("SessionFolderPath", sessionFolder);
    }

    private void WriteDemographicDataToFile()
    {

        if (string.IsNullOrEmpty(testerNameInput.text) ||
            string.IsNullOrEmpty(testerOccupationInput.text) ||
            string.IsNullOrEmpty(ageDropdown.value.ToString()) ||
            string.IsNullOrEmpty(genderDropdown.value.ToString()) ||
            (!lowExperienceToggle.isOn && !moderateExperienceToggle.isOn && !highExperienceToggle.isOn)
            )
        {
            return;
        }

        string path = CreateFile(testerNameInput.text);

        StringBuilder demographicDataString = new StringBuilder();
        demographicDataString.AppendLine("----------- Demographic Data -----------");
        demographicDataString.AppendLine("-Date:          " + DateTime.Now.Date.ToString());
        demographicDataString.AppendLine("-Tester:        " + testerNameInput.text);
        demographicDataString.AppendLine("-Age:           " + ageDropdown.options[ageDropdown.value].text);
        demographicDataString.AppendLine("-Gender:        " + genderDropdown.options[genderDropdown.value].text);
        demographicDataString.AppendLine("-Occupation:    " + testerOccupationInput.text);

        if (lowExperienceToggle.isOn)
        {
            demographicDataString.AppendLine("-VR Experience:  Low");
        }
        else if (moderateExperienceToggle.isOn)
        {
            demographicDataString.AppendLine("-VR Experience:  Moderate");
        }
        else
        {
            demographicDataString.AppendLine("-VR Experience:  High");
        }

        demographicDataString.AppendLine("----------- Demographic Data -----------");

        FileWriter.WriteToFile(path, demographicDataString.ToString());
        XRSettings.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private string CreateFile(string testerName)
    {
        string fileName = testerName + "_" + DateTime.Now.Date.ToString().Replace(" ", "__").Replace("/", "-").Replace(":", "-") + ".txt";
        
        string filePath = sessionFolder + "/" + fileName;

        FileStream creationStream = File.Create(filePath);
        creationStream.Close();

        PlayerPrefs.SetString("SessionFilePath", filePath);
        return filePath;
    }
}
