using Photon.Pun;
using UnityEngine;
using TMPro;
using VrPassing.Networking;
using UnityEngine.UI;

public class SceneUIManager : MonoBehaviourPun
{
    public string[] texts;
    public TextMeshProUGUI uiText;
    public Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    public void UpdateUIText(int step)
    {
        switch (step)
        {
            case 0:
                uiText.text = texts[0];
                startButton.gameObject.SetActive(true);
                break;
            case 1:
                uiText.text = texts[1];
                startButton.gameObject.SetActive(false);
                break;
            case 2:
                uiText.text = texts[2];
                startButton.gameObject.SetActive(false);
                break;

        }

    }

    public void OnStartButtonClicked()
    {
        this.photonView.RPC("PlayerReadyBroadcast", RpcTarget.MasterClient, PhotonNetwork.IsMasterClient ? 1 : 2);
    }

    [PunRPC]
    private void PlayerReadyBroadcast(int player)
    {
        if (player == 1)
        {
            ExperimentManager.Instance.playerOneReady = true;
        }
        else if (player == 2)
        {
            ExperimentManager.Instance.playerTwoReady = true;
        }
    }
}
