using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.SceneManagement;

public class StudyManager : MonoBehaviourPun
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
    private bool hasPlayerOneFinishedExperiment;
    [SerializeField]
    private bool hasPlayerTwoFinishedExperiment;

    [SerializeField]
    private RoomManager roomManager;

    private IEnumerator visualizationCoroutine;

    System.Random random;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetNextVisualization();
        }
        else
        {
            this.photonView.RPC("GetCurrentVisualizationMethod", RpcTarget.MasterClient);
        }

        if (visualizationCoroutine != null)
        {
            StopCoroutine(visualizationCoroutine);
            visualizationCoroutine = StartNextVisualization();
            StartCoroutine(visualizationCoroutine);
        }
        else
        {
            visualizationCoroutine = StartNextVisualization();
            StartCoroutine(visualizationCoroutine);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.transform);

        random = new System.Random();
        remainingVisualizations = new List<int>() { 1, 2, 3, 4 };

        if (PhotonNetwork.IsMasterClient)
        {
            GetNextVisualization();
        }
        else
        {
            this.photonView.RPC("GetCurrentVisualizationMethod", RpcTarget.MasterClient);
        }

        if (visualizationCoroutine != null)
        {
            StopCoroutine(visualizationCoroutine);
            visualizationCoroutine = StartNextVisualization();
            StartCoroutine(visualizationCoroutine);
        }
        else
        {
            visualizationCoroutine = StartNextVisualization();
            StartCoroutine(visualizationCoroutine);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                this.photonView.RPC("ExperimentFinished", RpcTarget.All, 1);
            }
            else
            {
                this.photonView.RPC("ExperimentFinished", RpcTarget.All, 2);
            }
        }
    }

    IEnumerator StartNextVisualization()
    {
        while (!hasPlayerOneFinishedExperiment && !hasPlayerTwoFinishedExperiment)
        {
            Debug.Log("Study not yet finished");
            yield return null;
        }

        Debug.Log("############ STUDY FINISHED!!");

        hasPlayerOneFinishedExperiment = false;
        hasPlayerTwoFinishedExperiment = false;

        if (remainingVisualizations.Count == 0)
        {
            roomManager.LeaveRoom();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Evaluation");
        }
    }

    private void GetNextVisualization()
    {
        int selectedVisualization = remainingVisualizations[random.Next(remainingVisualizations.Count)];
        remainingVisualizations.Remove(selectedVisualization);
        this.photonView.RPC("SetCurrentVisualizationMethod", RpcTarget.All, selectedVisualization);
    }

    [PunRPC]
    public void SetCurrentVisualizationMethod(int currentVisualizationMethod)
    {
        this.currentVisualizationMethod = currentVisualizationMethod;
        if (remainingVisualizations.Contains(currentVisualizationMethod))
        {
            remainingVisualizations.Remove(currentVisualizationMethod);
        }
    }

    [PunRPC]
    public void GetCurrentVisualizationMethod()
    {
        this.photonView.RPC("SetCurrentVisualizationMethod", RpcTarget.All, this.currentVisualizationMethod);
    }

    [PunRPC]
    public void ExperimentFinished(int finishedPlayer)
    {
        if (finishedPlayer == 1)
        {
            this.hasPlayerOneFinishedExperiment = true;

        }else if (finishedPlayer == 2)
        {
            this.hasPlayerTwoFinishedExperiment = true;
        }
    }
}
