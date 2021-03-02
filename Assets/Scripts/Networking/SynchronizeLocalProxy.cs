using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SynchronizeLocalProxy : MonoBehaviour
{
    [Header("VR Objects")]
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;

    [Header("Local Proxy Objects")]
    public GameObject headProxy;
    public Transform rightWristProxy;
    public Transform rightRootProxy;
    public Transform leftWristProxy;
    public Transform leftRootProxy;

    [Header("Hand Skeletons")]
    [Tooltip("Only the fields above need to be set, the ones under this are only for debugging purposes")]
    [SerializeField]
    private SteamVR_Behaviour_Skeleton _rightHandSkeleton;
    [SerializeField]
    private SteamVR_Behaviour_Skeleton _leftHandSkeleton;

    private bool _isLeftSkeletonSet = false;
    private bool _isRightSkeletonSet = false;

    [Header("Joints")]
    [SerializeField]
    private Transform[] _leftHandJoints;
    [SerializeField]
    private Transform[] _rightHandJoints;

    private void Start()
    {
        DontDestroyOnLoad(this.transform);

        _rightHandJoints = new Transform[26];
        _leftHandJoints = new Transform[26];

        _rightHandJoints[0] = rightRootProxy;
        Transform[] rightChildren = rightWristProxy.GetComponentsInChildren<Transform>();

        for (int i = 0; i < rightChildren.Length; i++)
        {
            _rightHandJoints[i + 1] = rightChildren[i];
        }


        _leftHandJoints[0] = leftRootProxy;
        Transform[] leftChildren = leftWristProxy.GetComponentsInChildren<Transform>();

        for (int i = 0; i < leftChildren.Length; i++)
        {
            _leftHandJoints[i + 1] = leftChildren[i];
        }

        _rightHandSkeleton = rightHand.GetComponentInChildren<SteamVR_Behaviour_Skeleton>();
        _leftHandSkeleton = leftHand.GetComponentInChildren<SteamVR_Behaviour_Skeleton>();
    }

    void Update()
    {
        #region Set Skeleton Scripts

        if (!_isLeftSkeletonSet)
        {
            if (_leftHandSkeleton == null )
            {
                _leftHandSkeleton = leftHand.GetComponentInChildren<SteamVR_Behaviour_Skeleton>();

                if (_leftHandSkeleton != null)
                {
                    _isLeftSkeletonSet = true;
                }
            }
            else
            {
                _isLeftSkeletonSet = true;
            }
        }

        if (!_isRightSkeletonSet)
        {
            if (_rightHandSkeleton == null)
            {
                _rightHandSkeleton = rightHand.GetComponentInChildren<SteamVR_Behaviour_Skeleton>();

                if (_rightHandSkeleton != null)
                {
                    _isRightSkeletonSet = true;
                }
            }
        }
        else
        {
            _isRightSkeletonSet = true;
        }

        #endregion

        headProxy.transform.localPosition = head.transform.position;
        headProxy.transform.localRotation = head.transform.rotation;

        UpdateLeftHandBones();

        UpdateRightHandBones();
        
    }
    private void UpdateLeftHandBones()
    {
        //Root
        _leftHandJoints[0].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.root).position;
        _leftHandJoints[0].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.root).rotation;

        //Wrist
        _leftHandJoints[1].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.wrist).position;
        _leftHandJoints[1].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.wrist).rotation;

        //INDEX
        _leftHandJoints[2].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMetacarpal).position;
        _leftHandJoints[2].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMetacarpal).rotation;

        _leftHandJoints[3].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexProximal).position;
        _leftHandJoints[3].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexProximal).rotation;

        _leftHandJoints[4].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMiddle).position;
        _leftHandJoints[4].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMiddle).rotation;

        _leftHandJoints[5].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexDistal).position;
        _leftHandJoints[5].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexDistal).rotation;

        _leftHandJoints[6].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexTip).position;
        _leftHandJoints[6].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexTip).rotation;

        //Middle
        _leftHandJoints[7].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMetacarpal).position;
        _leftHandJoints[7].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMetacarpal).rotation;

        _leftHandJoints[8].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleProximal).position;
        _leftHandJoints[8].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleProximal).rotation;

        _leftHandJoints[9].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMiddle).position;
        _leftHandJoints[9].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMiddle).rotation;

        _leftHandJoints[10].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleDistal).position;
        _leftHandJoints[10].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleDistal).rotation;

        _leftHandJoints[11].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleTip).position;
        _leftHandJoints[11].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleTip).rotation;

        //Pinky
        _leftHandJoints[12].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMetacarpal).position;
        _leftHandJoints[12].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMetacarpal).rotation;

        _leftHandJoints[13].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyProximal).position;
        _leftHandJoints[13].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyProximal).rotation;

        _leftHandJoints[14].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMiddle).position;
        _leftHandJoints[14].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMiddle).rotation;

        _leftHandJoints[15].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyDistal).position;
        _leftHandJoints[15].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyDistal).rotation;

        _leftHandJoints[16].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyTip).position;
        _leftHandJoints[16].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyTip).rotation;

        //Ring
        _leftHandJoints[17].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMetacarpal).position;
        _leftHandJoints[17].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMetacarpal).rotation;

        _leftHandJoints[18].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringProximal).position;
        _leftHandJoints[18].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringProximal).rotation;

        _leftHandJoints[19].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMiddle).position;
        _leftHandJoints[19].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMiddle).rotation;

        _leftHandJoints[20].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringDistal).position;
        _leftHandJoints[20].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringDistal).rotation;

        _leftHandJoints[21].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringTip).position;
        _leftHandJoints[21].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringTip).rotation;

        //Thumb
        _leftHandJoints[22].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMetacarpal).position;
        _leftHandJoints[22].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMetacarpal).rotation;

        _leftHandJoints[23].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbProximal).position;
        _leftHandJoints[23].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbProximal).rotation;

        _leftHandJoints[24].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMiddle).position;
        _leftHandJoints[24].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMiddle).rotation;

        _leftHandJoints[25].position = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbTip).position;
        _leftHandJoints[25].rotation = _leftHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbTip).rotation;
    } 

    private void UpdateRightHandBones()
    {
        _rightHandJoints[0].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.root).position;
        _rightHandJoints[0].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.root).rotation;

        _rightHandJoints[1].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.wrist).position;
        _rightHandJoints[1].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.wrist).rotation;

        //INDEX
        _rightHandJoints[2].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMetacarpal).position;
        _rightHandJoints[2].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMetacarpal).rotation;

        _rightHandJoints[3].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexProximal).position;
        _rightHandJoints[3].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexProximal).rotation;

        _rightHandJoints[4].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMiddle).position;
        _rightHandJoints[4].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexMiddle).rotation;

        _rightHandJoints[5].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexDistal).position;
        _rightHandJoints[5].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexDistal).rotation;

        _rightHandJoints[6].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexTip).position;
        _rightHandJoints[6].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.indexTip).rotation;

        //Middle
        _rightHandJoints[7].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMetacarpal).position;
        _rightHandJoints[7].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMetacarpal).rotation;

        _rightHandJoints[8].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleProximal).position;
        _rightHandJoints[8].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleProximal).rotation;

        _rightHandJoints[9].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMiddle).position;
        _rightHandJoints[9].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleMiddle).rotation;

        _rightHandJoints[10].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleDistal).position;
        _rightHandJoints[10].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleDistal).rotation;

        _rightHandJoints[11].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleTip).position;
        _rightHandJoints[11].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.middleTip).rotation;

        //Pinky
        _rightHandJoints[12].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMetacarpal).position;
        _rightHandJoints[12].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMetacarpal).rotation;

        _rightHandJoints[13].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyProximal).position;
        _rightHandJoints[13].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyProximal).rotation;

        _rightHandJoints[14].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMiddle).position;
        _rightHandJoints[14].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyMiddle).rotation;

        _rightHandJoints[15].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyDistal).position;
        _rightHandJoints[15].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyDistal).rotation;

        _rightHandJoints[16].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyTip).position;
        _rightHandJoints[16].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.pinkyTip).rotation;

        //Ring
        _rightHandJoints[17].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMetacarpal).position;
        _rightHandJoints[17].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMetacarpal).rotation;

        _rightHandJoints[18].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringProximal).position;
        _rightHandJoints[18].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringProximal).rotation;

        _rightHandJoints[19].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMiddle).position;
        _rightHandJoints[19].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringMiddle).rotation;

        _rightHandJoints[20].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringDistal).position;
        _rightHandJoints[20].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringDistal).rotation;

        _rightHandJoints[21].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringTip).position;
        _rightHandJoints[21].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.ringTip).rotation;

        //Thumb
        _rightHandJoints[22].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMetacarpal).position;
        _rightHandJoints[22].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMetacarpal).rotation;

        _rightHandJoints[23].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbProximal).position;
        _rightHandJoints[23].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbProximal).rotation;

        _rightHandJoints[24].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMiddle).position;
        _rightHandJoints[24].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbMiddle).rotation;

        _rightHandJoints[25].position = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbTip).position;
        _rightHandJoints[25].rotation = _rightHandSkeleton.GetBone(SteamVR_Skeleton_JointIndexes.thumbTip).rotation;
    }

}
