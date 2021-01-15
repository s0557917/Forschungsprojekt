﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TextMeshProUGUI statusText;
    [SerializeField]
    private int _playerNumber;

    private byte _maxPlayersInRoom = 2;
    private bool _isConnecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        if (_isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
        }

        statusText.text = "Connected to Master";
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        statusText.text = "Disconnected - " + cause.ToString();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        statusText.text = "Joining a Room failed";

        PhotonNetwork.JoinOrCreateRoom("ExperimentRoom", new RoomOptions { MaxPlayers = _maxPlayersInRoom}, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined a room succesfully - " + PhotonNetwork.CurrentRoom.Name;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1 && PhotonNetwork.IsMasterClient)
        {
            statusText.text = "We load the 'Room for 1' ";
            //PlayerPrefs.SetInt("PlayerNumber", 1);
            PhotonNetwork.LoadLevel("Main");
        }
        else
        {
            //PlayerPrefs.SetInt("PlayerNumber", 2);
        }
    }

    #endregion
}