using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandBoneProxy : MonoBehaviour, IPunObservable
{
    public GameObject[] boneVisualizers;
    public Transform[] bones;


    // Start is called before the first frame update
    void Start()
    {
        boneVisualizers = new GameObject[26];

        for (int i = 0; i < boneVisualizers.Length; i++)
        {
            GameObject visualizer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            visualizer.name = "" + i;
            visualizer.transform.localScale = new Vector3(.01f, .01f, .01f);
            Destroy(visualizer.GetComponent<BoxCollider>());

            if (i == 0)
            {
                visualizer.GetComponent<Renderer>().material.color = Color.blue;
            }
            else
            {
                visualizer.GetComponent<Renderer>().material.color = Color.red;
            }

            boneVisualizers[i] = visualizer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bones.Length; i++)
        {
            boneVisualizers[i].transform.position = bones[i].position;
            boneVisualizers[i].transform.rotation = bones[i].rotation;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            bones = (Transform[]) stream.ReceiveNext();
        }
    }

}
