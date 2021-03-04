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

    public GameObject studyManager;

    [SerializeField]
    private GameObject teleportAreaOne;
    [SerializeField]
    private GameObject teleportAreaTwo;

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

        if (PhotonNetwork.IsMasterClient)
        {

            InstantiateStudyManager();

            vrPlayer.transform.position = new Vector3(0, 0, -1);
            vrPlayer.transform.rotation = Quaternion.Euler(0, 0, 0);

            teleportAreaOne.SetActive(true);

        }
        else
        {

            vrPlayer.transform.position = new Vector3(0, 0, 1);
            vrPlayer.transform.rotation = Quaternion.Euler(0, 180, 0);

            teleportAreaTwo.SetActive(true);
        }

        GameObject instantiatedPlayerProxy = PhotonNetwork.Instantiate(playerProxy.name, vrPlayer.transform.position, vrPlayer.transform.rotation, 0);

        SynchronizeLocalProxy synchronizer = instantiatedPlayerProxy.GetComponent<SynchronizeLocalProxy>();
        synchronizer.head = GameObject.FindGameObjectWithTag("Head_VR");
        synchronizer.rightHand = GameObject.FindGameObjectWithTag("RightHand_VR");
        synchronizer.leftHand = GameObject.FindGameObjectWithTag("LeftHand_VR");
    }

    private void InstantiateStudyManager()
    {
        if (!GameObject.FindGameObjectWithTag("StudyManager"))
        {
            PhotonNetwork.Instantiate(studyManager.name, Vector3.zero, Quaternion.identity, 0);
        }
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
}
