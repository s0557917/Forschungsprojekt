using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.Events;
using VrPassing.Utilities;

namespace VrPassing.Networking
{
    public class ExperimentManager : MonoBehaviourPun
    {
        private static ExperimentManager _instance;
        public static ExperimentManager Instance
        {
            get { return _instance; }
        }

        public CubeManager cubeManager;
        public GameObject sceneUI;
        int player;
        int currentStep = 0;

        [SerializeField] private bool _playerOneReady = false;
        [SerializeField] private bool _playerTwoReady = false;
        public bool playerOneReady
        {
            get
            {
                return _playerOneReady;
            }
            set
            {
                _playerOneReady = value;
                if (_playerTwoReady && _playerOneReady)
                {
                    StartExperimentRun();
                }
            }
        }

        public bool playerTwoReady
        {
            get
            {
                return _playerTwoReady;
            }
            set
            {
                _playerTwoReady = value;
                if (_playerTwoReady && _playerOneReady)
                {
                    StartExperimentRun();
                }
            }
        }

        UnityEvent countdownHasFinished;
        UnityEvent experimentRunFinished;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }

            _instance = this;

            if (PhotonNetwork.IsMasterClient)
            {
                player = 1;
            }
            else
            {
                player = 2;
                _instance.enabled = false;
                cubeManager.enabled = false;
            }
        }

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                player = 1;
            }
            else
            {
                player = 2;
                _instance.enabled = false;
            }

            countdownHasFinished = new UnityEvent();
            experimentRunFinished = new UnityEvent();

            countdownHasFinished.AddListener(StartExperimentRun);

            StartTrialStep();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R) && PhotonNetwork.IsMasterClient)
            {
                cubeManager.GetNextVisualization();
            }
        }

        #region Step Methods
        private void StartTrialStep()
        {
            currentStep = 0;
            StartCoroutine(TimeManager.StartCountdown(countdownHasFinished));
            cubeManager.GetNextVisualization();
        }

        private void StartExperimentRun()
        {
            currentStep = 1;
        }

        private void StartQuestionnaireStep()
        {
            currentStep = 2;
        }
        #endregion


        #region RPC's

        #endregion

        #region Helper Methods

        private void InstantiateSceneUI()
        {
            if (player == 1)
            {
                PhotonNetwork.Instantiate(sceneUI.name, Vector3.zero, Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(sceneUI.name, Vector3.zero, Quaternion.Euler(0, 180, 0));
            }
        }

        #endregion





        //private int currentVisualizationMethod;
        //private List<int> remainingVisualizations;

        //private bool hasPlayerOneFinishedExperiment = false;
        //private bool hasPlayerTwoFinishedExperiment = false;

        //private RoomManager roomManager;

        //private System.Random random;

        ////private DateTime startingTime;
        ////private DateTime endingTime;
        ////private TimeSpan experimentDuration;

        //private int player;

        //EvaluationManager evaluationManager;

        //private void Awake()
        //{
        //    roomManager = GameObject.FindObjectOfType<RoomManager>();
        //}

        //void Start()
        //{
        //    evaluationManager = this.GetComponent<EvaluationManager>();

        //    random = new System.Random();
        //    remainingVisualizations = new List<int> { 1, 2, 3, 4 };

        //    if (PhotonNetwork.IsMasterClient)
        //    {
        //        player = 1;

        //    }
        //    else
        //    {
        //        player = 2;
        //        this.photonView.RPC("GetCurrentVisualizationMethod", RpcTarget.MasterClient);
        //    }
        //}

        //public void StartTrial()
        //{

        //}

        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.H))
        //    {
        //        if (player == 1)
        //        {
        //            this.photonView.RPC("ExperimentFinished", RpcTarget.All, 1);
        //        }
        //        else
        //        {
        //            this.photonView.RPC("ExperimentFinished", RpcTarget.All, 2);
        //        }
        //    }
        //}

        //private void GetNextVisualization()
        //{
        //    int selectedVisualization = remainingVisualizations[random.Next(remainingVisualizations.Count)];
        //    remainingVisualizations.Remove(selectedVisualization);
        //    this.photonView.RPC("SetCurrentVisualizationMethod", RpcTarget.All, selectedVisualization);

        //    if (player == 1)
        //    {
        //        startingTime = DateTime.Now;
        //    }
        //}

        //[PunRPC]
        //public void SetCurrentVisualizationMethod(int currentVisualizationMethod)
        //{
        //    this.currentVisualizationMethod = currentVisualizationMethod;
        //    if (remainingVisualizations.Contains(currentVisualizationMethod))
        //    {
        //        remainingVisualizations.Remove(currentVisualizationMethod);
        //    }
        //}

        //[PunRPC]
        //public void GetCurrentVisualizationMethod()
        //{
        //    this.photonView.RPC("SetCurrentVisualizationMethod", RpcTarget.All, this.currentVisualizationMethod);
        //}

        //[PunRPC]
        //public void ExperimentFinished(int finishedPlayer)
        //{
        //    if (finishedPlayer == 1)
        //    {
        //        this.hasPlayerOneFinishedExperiment = true;
        //    }
        //    else if (finishedPlayer == 2)
        //    {
        //        this.hasPlayerTwoFinishedExperiment = true;
        //    }

        //    if (hasPlayerOneFinishedExperiment == true && hasPlayerTwoFinishedExperiment == true)
        //    {
        //        if (player == 1)
        //        {
        //            endingTime = DateTime.Now;
        //            experimentDuration = (endingTime - startingTime);
        //        }
        //        else
        //        {
        //            this.photonView.RPC("GetExperimentTimes", RpcTarget.MasterClient);
        //        }

        //        SetupAndLaunchEvaluation();

        //        //Write times to file
        //        Debug.Log("Start: " + startingTime.ToLongTimeString() + " Ending: " + endingTime.ToLongTimeString() + " = " + experimentDuration.TotalMinutes);
        //    }
        //}

        //private void SetupAndLaunchEvaluation()
        //{
        //    hasPlayerOneFinishedExperiment = false;
        //    hasPlayerTwoFinishedExperiment = false;

        //    startingTime = new DateTime();
        //    endingTime = new DateTime();
        //    experimentDuration = new TimeSpan();

        //    evaluationManager.LaunchEvaluation(player, roomManager);
        //}

        //[PunRPC]
        //public void SetExperimentTimes(string startingTime, string endingTime)
        //{
        //    this.startingTime = Convert.ToDateTime(startingTime);
        //    this.endingTime = Convert.ToDateTime(endingTime);
        //    this.experimentDuration = this.endingTime - this.startingTime;
        //}

        //[PunRPC]
        //public void GetExperimentTimes()
        //{
        //    this.photonView.RPC("SetExperimentTimes", RpcTarget.All, this.startingTime.ToString(), this.endingTime.ToString());
        //}


    }
}
