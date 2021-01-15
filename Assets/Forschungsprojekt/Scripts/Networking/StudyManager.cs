using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class StudyManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textPlayerOne;
    public TextMeshProUGUI textPlayerTwo;

    [SerializeField]
    private int _playerNumber;
    [SerializeField]
    private GameObject vrPlayer;
    [SerializeField]
    private GameObject playerProxy;

    public void Start()
    {
        vrPlayer = GameObject.FindGameObjectWithTag("VRPlayer");

        if (vrPlayer == null)
        {
            Debug.Log("VRPlayer IS NULL!");
        }
        else
        {
            Debug.Log("VRPLAYER:: " + vrPlayer.name);
        }

        if (PlayerPrefs.HasKey("PlayerNumber"))
        {
            _playerNumber = PlayerPrefs.GetInt("PlayerNumber");
        }

        //NEEDS TO BE TESTED
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            vrPlayer.transform.position = new Vector3(0, 0, 0.5f);
            vrPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);
            vrPlayer.transform.forward = -Vector3.forward;

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            vrPlayer.transform.position = new Vector3(0, 0, -1);
            vrPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);
            vrPlayer.transform.forward = Vector3.forward;
        }

        PhotonNetwork.Instantiate(playerProxy.name, vrPlayer.transform.position, vrPlayer.transform.rotation, 0);   

        textPlayerOne.text = "Connected as Player " + _playerNumber;
        textPlayerTwo.text = "Connected as Player " + _playerNumber;
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(2);
    }
}
