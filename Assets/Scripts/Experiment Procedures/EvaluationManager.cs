using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    public Vector3 evalRoomOnePlayerPosition;
    public Vector3 evalRoomOnePlayerRotation;
    public Vector3 evalRoomTwoPlayerPosition;
    public Vector3 evalRoomTwoPlayerRotation;

    private GameObject vrPlayer;
    private RoomManager roomManager;

    int player;

    private void Start()
    {
        vrPlayer = GameObject.FindGameObjectWithTag("VRPlayer");
    }

    public void LaunchEvaluation(int player, RoomManager roomManager)
    {
        this.roomManager = roomManager;
        this.player = player;

        if(player == 1)
        {
            vrPlayer.transform.position = evalRoomOnePlayerPosition;
            vrPlayer.transform.eulerAngles = evalRoomOnePlayerRotation;
        } else if(player == 2)
        {
            vrPlayer.transform.position = evalRoomTwoPlayerPosition;
            vrPlayer.transform.eulerAngles = evalRoomTwoPlayerRotation;
        }

    }

    public void ReturnToExperiment()
    {
        roomManager.ResetPlayerToOriginalPosition(player);
    }
}
