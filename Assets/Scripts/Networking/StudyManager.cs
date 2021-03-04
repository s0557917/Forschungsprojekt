using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

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
    
    private int currentVisualizationMethod;
    private List<int> remainingVisualizations;

    private bool hasPlayerOneFinishedExperiment;
    private bool hasPlayerTwoFinishedExperiment;

    private RoomManager roomManager;

    System.Random random;

    private DateTime startingTime;
    private DateTime endingTime;
    private TimeSpan experimentDuration;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            startingTime = new DateTime();
            endingTime = new DateTime();
            experimentDuration = new TimeSpan();

            hasPlayerOneFinishedExperiment = false;
            hasPlayerTwoFinishedExperiment = false;

            if (PhotonNetwork.IsMasterClient)
            {
                GetNextVisualization();
            }
            else
            {
                this.photonView.RPC("GetCurrentVisualizationMethod", RpcTarget.MasterClient);
            }
        }

    }

    void Start()
    {
        hasPlayerOneFinishedExperiment = false;
        hasPlayerTwoFinishedExperiment = false;

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

    private void GetNextVisualization()
    {
        int selectedVisualization = remainingVisualizations[random.Next(remainingVisualizations.Count)];
        remainingVisualizations.Remove(selectedVisualization);
        this.photonView.RPC("SetCurrentVisualizationMethod", RpcTarget.All, selectedVisualization);

        if (PhotonNetwork.IsMasterClient)
        {
            startingTime = DateTime.Now;
        }
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
        }
        else if (finishedPlayer == 2)
        {
            this.hasPlayerTwoFinishedExperiment = true;
        }

        if (hasPlayerOneFinishedExperiment == true && hasPlayerTwoFinishedExperiment == true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                endingTime = DateTime.Now;
                experimentDuration = (endingTime - startingTime);
            }
            else
            {
                this.photonView.RPC("GetExperimentTimes", RpcTarget.MasterClient);
            }

            PhotonNetwork.LoadLevel("Evaluation");
        }
    }

    [PunRPC]
    public void SetExperimentTimes(string startingTime, string endingTime)
    {
        this.startingTime = Convert.ToDateTime(startingTime);
        this.endingTime = Convert.ToDateTime(endingTime);
        this.experimentDuration = this.endingTime - this.startingTime;
    }

    [PunRPC]
    public void GetExperimentTimes()
    {
        this.photonView.RPC("SetExperimentTimes", RpcTarget.All, this.startingTime.ToString(), this.endingTime.ToString());
    }

}
