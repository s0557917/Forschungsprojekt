using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class StudyManager : MonoBehaviour
{
    public static StudyManager _instance;
    public static StudyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StudyManager>();

                if (_instance == null)
                {
                    GameObject studyManagerObject = new GameObject("Study Manager");
                    _instance = studyManagerObject.AddComponent<StudyManager>();
                }
            }

            return _instance;
        }

    }
    
    [SerializeField]
    private int currentVisualizationMethod;
    [SerializeField]
    private List<int> remainingVisualizations;

    [SerializeField]
    private bool isPlayerOneFinished = false;
    private bool isPlayerTwoFinished = false;

    System.Random random;

    void Start()
    {
        random = new System.Random();
        remainingVisualizations = new List<int>() { 1, 2, 3, 4 };

        currentVisualizationMethod = GetNextVisualization();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetNextVisualization();
        }
    }

    IEnumerator StartNextVisualization()
    {
        while (!isPlayerOneFinished || !isPlayerTwoFinished)
        {
            yield return null;
        }

        isPlayerOneFinished = false;
        isPlayerTwoFinished = false;

        //currentVisualizationMethod = GetNextVisualization();

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Main");
        }
    }

    private int GetNextVisualization()
    {
        int selectedVisualization = remainingVisualizations[random.Next(remainingVisualizations.Count)];
        Debug.Log("SELECTED:: " + selectedVisualization);
        remainingVisualizations.Remove(selectedVisualization);

        return selectedVisualization;
    }
}
