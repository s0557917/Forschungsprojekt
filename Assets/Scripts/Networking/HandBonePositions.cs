using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class HandBonePositions : MonoBehaviour, IPunObservable
{
    public SteamVR_Behaviour_Skeleton behaviourSkeleton;

    public Transform[] bones;
    

    // Start is called before the first frame update
    void Start()
    {
        behaviourSkeleton = this.GetComponent<SteamVR_Behaviour_Skeleton>();

        bones = new Transform[26];

    }   

    // Update is called once per frame
    void Update()
    {
        GetBoneTransforms();
    }

    private void GetBoneTransforms()
    {
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i] = behaviourSkeleton.GetBone(i);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bones);
        }
    }
}
