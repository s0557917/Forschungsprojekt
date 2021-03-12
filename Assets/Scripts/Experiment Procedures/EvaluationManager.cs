using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationManager : MonoBehaviour
{
    Vector3 evalRoomOnePlayerPosition;
    Vector3 evalRoomOnePlayerRotation;
    Vector3 evalRoomTwoPlayerPosition;
    Vector3 evalRoomTwoPlayerRotation;

    private GameObject vrPlayer;
    private RoomManager roomManager;

    int player;

    private void Start()
    {
        evalRoomOnePlayerPosition = new Vector3(25, 0.1f, -1 );
        evalRoomOnePlayerRotation = Vector3.zero;
        evalRoomTwoPlayerPosition = new Vector3(50, 0.1f, -1);
        evalRoomTwoPlayerRotation = Vector3.zero;

        vrPlayer = GameObject.FindGameObjectWithTag("VRPlayer");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            //RPC TO SYNCHRONIZE RETURN
            ReturnToExperiment();
        }
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
