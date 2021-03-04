using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textPlayerOne;
    public TextMeshProUGUI textPlayerTwo;

    [SerializeField]
    private GameObject teleportAreaOne;
    [SerializeField]
    private GameObject teleportAreaTwo;

    [SerializeField]
    private GameObject vrPlayer;
    [SerializeField]
    private GameObject playerProxy;

    Vector3 startingPositionPlayerOne;
    Vector3 startingRotationPlayerOne;
    Vector3 startingPositionPlayerTwo;
    Vector3 startingRotationPlayerTwo;

    public void Start()
    {
        startingPositionPlayerOne = new Vector3(0, 0, -1);
        startingRotationPlayerOne = new Vector3(0, 0, 0);
        startingPositionPlayerTwo = new Vector3(0, 0, 1);
        startingRotationPlayerTwo = new Vector3(0, 180, 0);

        vrPlayer = GameObject.FindGameObjectWithTag("VRPlayer");

        if (PhotonNetwork.IsMasterClient)
        {
            vrPlayer.transform.position = startingPositionPlayerOne;
            vrPlayer.transform.eulerAngles = startingRotationPlayerOne;

            teleportAreaOne.SetActive(true);

        }
        else
        {
            vrPlayer.transform.position = startingPositionPlayerTwo;
            vrPlayer.transform.eulerAngles = startingRotationPlayerTwo;

            teleportAreaTwo.SetActive(true);
        }

        GameObject instantiatedPlayerProxy = PhotonNetwork.Instantiate(playerProxy.name, vrPlayer.transform.position, vrPlayer.transform.rotation, 0);

        SynchronizeLocalProxy synchronizer = instantiatedPlayerProxy.GetComponent<SynchronizeLocalProxy>();
        synchronizer.head = GameObject.FindGameObjectWithTag("Head_VR");
        synchronizer.rightHand = GameObject.FindGameObjectWithTag("RightHand_VR");
        synchronizer.leftHand = GameObject.FindGameObjectWithTag("LeftHand_VR");
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
        SceneManager.LoadScene(0);
    }

    private GameObject GetChildWithTag(string tag)
    {
        foreach (Transform child in vrPlayer.transform)
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    public void ResetPlayerToOriginalPosition(int player)
    {
        if (player == 1)
        {
            vrPlayer.transform.position = startingPositionPlayerOne;
            vrPlayer.transform.eulerAngles = startingRotationPlayerOne;
        }
        else
        {
            vrPlayer.transform.position = startingPositionPlayerTwo;
            vrPlayer.transform.eulerAngles = startingRotationPlayerTwo;
        }
    }
}
