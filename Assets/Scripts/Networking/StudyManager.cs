using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class StudyManager : MonoBehaviourPun
{
    private int currentVisualizationMethod;
    private List<int> remainingVisualizations;

    private bool hasPlayerOneFinishedExperiment;
    private bool hasPlayerTwoFinishedExperiment;

    private RoomManager roomManager;

    private System.Random random;

    private DateTime startingTime;
    private DateTime endingTime;
    private TimeSpan experimentDuration;

    private int player;

    EvaluationManager evaluationManager;

    private void Awake()
    {
        roomManager = GameObject.FindObjectOfType<RoomManager>();
    }
    
    void Start()
    {
        evaluationManager = this.GetComponent<EvaluationManager>();

        hasPlayerOneFinishedExperiment = false;
        hasPlayerTwoFinishedExperiment = false;

        random = new System.Random();

        if (PhotonNetwork.IsMasterClient)
        {
            player = 1;
            GetNextVisualization();
        }
        else
        {
            player = 2;
            this.photonView.RPC("GetCurrentVisualizationMethod", RpcTarget.MasterClient);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (player == 1)
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

        if (player == 1)
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
            if (player == 1)
            {
                endingTime = DateTime.Now;
                experimentDuration = (endingTime - startingTime);
            }
            else
            {
                this.photonView.RPC("GetExperimentTimes", RpcTarget.MasterClient);
            }

            SetupAndLaunchEvaluation();
        }
    }

    private void SetupAndLaunchEvaluation()
    {
        hasPlayerOneFinishedExperiment = false;
        hasPlayerTwoFinishedExperiment = false;

        startingTime = new DateTime();
        endingTime = new DateTime();
        experimentDuration = new TimeSpan();

        evaluationManager.LaunchEvaluation(player, roomManager);
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
