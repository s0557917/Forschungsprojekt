using System.Collections;
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
            PhotonNetwork.JoinOrCreateRoom("ExperimentRoom", new RoomOptions { MaxPlayers = _maxPlayersInRoom }, TypedLobby.Default);
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
        Debug.Log("Joining a Room failed");
        PhotonNetwork.CreateRoom("ExperimentRoom", new RoomOptions { MaxPlayers = _maxPlayersInRoom}, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined a room succesfully - " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient && (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount))
        {
            statusText.text = "We load the 'Room for 1' ";
            Debug.Log("LOADED MAIN LEVEL");
            PhotonNetwork.LoadLevel("Main");
        }


    }

    #endregion
}
