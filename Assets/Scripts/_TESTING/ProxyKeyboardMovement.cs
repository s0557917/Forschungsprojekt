using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ProxyKeyboardMovement : MonoBehaviourPunCallbacks
{
    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;

    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                head.transform.position += new Vector3(0, 0.5f, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                head.transform.position -= new Vector3(0, 0.5f, 0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                leftHand.transform.position += new Vector3(0, 0.5f, 0);
                rightHand.transform.position += new Vector3(0, 0.5f, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                leftHand.transform.position -= new Vector3(0, 0.5f, 0);
                rightHand.transform.position -= new Vector3(0, 0.5f, 0);
            }
        }
    }
}
