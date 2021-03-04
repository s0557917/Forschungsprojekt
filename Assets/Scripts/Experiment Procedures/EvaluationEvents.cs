using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationEvents : MonoBehaviour
{
    public void RejoinExperimentRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }
}
