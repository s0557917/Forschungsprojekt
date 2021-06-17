using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;
using VrPassing.Events;

namespace VrPassing.Grabbing
{
    public class Grabbing : MonoBehaviour
    {
        [SerializeField] private SteamVR_Input_Sources handType;
        private SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");
        [SerializeField] private float gripStrength;

        private HandSide currentHandSide = HandSide.Undefined;
        private HandPhysics handPhysics;
        private HandCollider handCollider;
        private IEnumerator colliderEnumerator;
        private UnityEvent handColliderSet;

        private FingerInteractionNotification thumbInteractionNotification;
        private FingerInteractionNotification indexInteractionNotification;
        private FingerInteractionNotification middleInteractionNotification;
        private FingerInteractionNotification ringInteractionNotification;
        private FingerInteractionNotification pinkyInteractionNotification;

        [SerializeField] private bool canGrabObject = false;

        [SerializeField] private int touchingFingers = 0;

        [SerializeField] private bool isThumbTouchingCube = false;
        [SerializeField] private bool isIndexTouchingCube = false;
        [SerializeField] private bool isMiddleTouchingCube = false;
        [SerializeField] private bool isRingTouchingCube = false;
        [SerializeField] private bool isPinkyTouchingCube = false;

        [SerializeField]GameObject currentCollidingObject;

        private enum HandSide
        {
            Undefined,
            Right,
            Left
        }

        void Start()
        {
            handColliderSet = new UnityEvent();
            handColliderSet.AddListener(() => SetupFingerEvents());
            GetHandCollider();
        }

        private void GetHandCollider()
        {
            handPhysics = this.GetComponent<HandPhysics>();
            colliderEnumerator = WaitAndGetCollider();
            StartCoroutine(colliderEnumerator);
            currentHandSide = this.gameObject.name.Contains("Right") ? HandSide.Right : HandSide.Left;
        }

        private IEnumerator WaitAndGetCollider()
        {
            WaitForEndOfFrame waitEndOfFrame = new WaitForEndOfFrame();

            while (handCollider == null)
            {
                handCollider = handPhysics.handCollider;
                yield return waitEndOfFrame;
            }

            handColliderSet.Invoke();
            yield return null;
        }

        private void SetupFingerEvents()
        {
            thumbInteractionNotification = new FingerInteractionNotification();
            indexInteractionNotification = new FingerInteractionNotification();
            middleInteractionNotification = new FingerInteractionNotification();
            ringInteractionNotification = new FingerInteractionNotification();
            pinkyInteractionNotification = new FingerInteractionNotification();

            thumbInteractionNotification.AddListener(ThumbListener);
            indexInteractionNotification.AddListener(IndexListener);
            middleInteractionNotification.AddListener(MiddleListener);
            ringInteractionNotification.AddListener(RingListener);
            pinkyInteractionNotification.AddListener(PinkyListener);

            //HandCollisionDetection interactionNotifier = handCollider.gameObject.AddComponent<HandCollisionDetection>();

            //interactionNotifier.SetupFingerEvents(thumbInteractionNotification, 
            //    indexInteractionNotification, 
            //    middleInteractionNotification, 
            //    ringInteractionNotification, 
            //    pinkyInteractionNotification
            //);
        }

        private void FixedUpdate()
        {
            gripStrength = gripSqueeze.GetAxis(handType);
            touchingFingers = getTouchingFingersCount();
            if (gripStrength >= 0.45 && CheckIfHandCanGrab())
            {
                canGrabObject = true;
                currentCollidingObject.transform.parent = this.transform;
                Debug.Log("CAN GRAB OBJECT");
            }
        }

        private bool CheckIfHandCanGrab()
        {
            //if (isThumbTouchingCube && (isIndexTouchingCube|| isMiddleTouchingCube || isRingTouchingCube || isPinkyTouchingCube))
            if (isThumbTouchingCube && getTouchingFingersCount() < 1 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int getTouchingFingersCount()
        {
            return Convert.ToInt32(isIndexTouchingCube) + 
                Convert.ToInt32(isMiddleTouchingCube) + 
                Convert.ToInt32(isRingTouchingCube) + 
                Convert.ToInt32(isPinkyTouchingCube);
        }

        private void OnEnable()
        {
            handColliderSet?.RemoveAllListeners();
            if (colliderEnumerator != null)
            {
                StopCoroutine(colliderEnumerator);
                colliderEnumerator = null;
            }
        }

        private void OnDisable()
        {
            handColliderSet?.RemoveAllListeners();
            if (colliderEnumerator != null)
            {
                StopCoroutine(colliderEnumerator);
                colliderEnumerator = null;
            }
        }

        #region FingerListeners

        private void ThumbListener(bool isTouchingCube, GameObject collisionObject)
        {
            isThumbTouchingCube = isTouchingCube;
        }

        private void IndexListener(bool isTouchingCube, GameObject collisionObject)
        {

            if (collisionObject != currentCollidingObject )
            {
                currentCollidingObject = collisionObject;
            }
            isIndexTouchingCube = isTouchingCube;
        }

        private void MiddleListener(bool isTouchingCube, GameObject collisionObject)
        {

            if (collisionObject != currentCollidingObject)
            {
                currentCollidingObject = collisionObject;
            }

            isMiddleTouchingCube = isTouchingCube;
        }

        private void RingListener(bool isTouchingCube, GameObject collisionObject)
        {

            if (collisionObject != currentCollidingObject)
            {
                currentCollidingObject = collisionObject;
            }

            isRingTouchingCube = isTouchingCube;
        }

        private void PinkyListener(bool isTouchingCube, GameObject collisionObject)
        {

            if (collisionObject != currentCollidingObject)
            {
                currentCollidingObject = collisionObject;
            }
            isPinkyTouchingCube = isTouchingCube;
        }
        #endregion FingerListeners

    }
}
