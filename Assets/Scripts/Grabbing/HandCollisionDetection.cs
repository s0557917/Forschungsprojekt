using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using VrPassing.Events;

namespace VrPassing.Grabbing
{
    public class HandCollisionDetection : MonoBehaviour
    {
        [SerializeField] private int cubeLayer = 11;
        [SerializeField] private HandCollider handCollider;
        [SerializeField] private Grabbing grabbingController;
        //private FingerInteractionNotification thumbFingerEvent;
        //private FingerInteractionNotification indexFingerEvent;
        //private FingerInteractionNotification middleFingerEvent;
        //private FingerInteractionNotification ringFingerEvent;
        //private FingerInteractionNotification pinkyFingerEvent;
        List<ContactPoint> contactPointsToDelete = new List<ContactPoint>();
        private List<ContactPoint> lastContactPoints;
        public List<GameObject> contacts;
        public int contactCount = 0;
        //public void SetupFingerEvents(FingerInteractionNotification thumbFingerEvent, 
        //    FingerInteractionNotification indexFingerEvent,
        //    FingerInteractionNotification middleFingerEvent,
        //    FingerInteractionNotification ringFingerEvent,
        //    FingerInteractionNotification pinkyFingerEvent
        //)
        //{
        //    this.thumbFingerEvent = thumbFingerEvent;
        //    this.indexFingerEvent = indexFingerEvent;
        //    this.middleFingerEvent = middleFingerEvent;
        //    this.ringFingerEvent = ringFingerEvent;
        //    this.pinkyFingerEvent = pinkyFingerEvent;
        //}

        private void Start()
        {
            grabbingController = handCollider.hand.gameObject.GetComponent<Grabbing>();
            contacts = new List<GameObject>();
            lastContactPoints = new List<ContactPoint>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == cubeLayer && collision.contacts.Length > 0)
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    if (grabbingController.grabbedObject == null)
                    {
                        grabbingController.grabbedObject = contact.otherCollider.gameObject;
                    }

                    if (grabbingController.grabbedObject != null && contact.otherCollider.gameObject == grabbingController.grabbedObject)
                    {
                        if (!lastContactPoints.Contains(contact))
                        {
                            lastContactPoints.Add(contact);
                        }

                        if (!contacts.Contains(contact.thisCollider.gameObject))
                        {
                            contacts.Add(contact.thisCollider.gameObject);
                        }

                        string collidingObjectName = contact.thisCollider.gameObject.name;
                        if (collidingObjectName.Contains("thumb"))
                        {
                            grabbingController.isThumbTouchingCube = true;
                            //thumbFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (collidingObjectName.Contains("index_2"))
                        {
                            grabbingController.isIndexTouchingCube = true;
                            //indexFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (collidingObjectName.Contains("middle_2"))
                        {
                            grabbingController.isMiddleTouchingCube = true;
                            //middleFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (collidingObjectName.Contains("ring_2"))
                        {
                            grabbingController.isRingTouchingCube = true;
                            //ringFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (collidingObjectName.Contains("pinky_2"))
                        {
                            grabbingController.isPinkyTouchingCube = true;
                            //pinkyFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                    }
                    else
                    {
                        Debug.Log("SOMETHING IS NULL " + grabbingController.grabbedObject + " -- " + (contact.otherCollider.gameObject == grabbingController.grabbedObject));
                    }
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == cubeLayer)
            {
                foreach (ContactPoint contact in lastContactPoints)
                {
                    if (contact.otherCollider.gameObject == grabbingController.grabbedObject)
                    {
                        if (lastContactPoints.Contains(contact))
                        {
                            contactPointsToDelete.Add(contact);
                        }

                        contacts.RemoveAll(contactingObject => contactingObject == contact.thisCollider.gameObject);

                        if (contact.thisCollider.gameObject.name.Contains("thumb"))
                        {
                            grabbingController.isThumbTouchingCube = false;
                            //thumbFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (contact.thisCollider.gameObject.name.Contains("index_2"))
                        {
                            grabbingController.isIndexTouchingCube = false;
                            //indexFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (contact.thisCollider.gameObject.name.Contains("middle_2"))
                        {
                            grabbingController.isMiddleTouchingCube = false;
                            //middleFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (contact.thisCollider.gameObject.name.Contains("ring_2"))
                        {
                            grabbingController.isRingTouchingCube = false;
                            //ringFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                        else if (contact.thisCollider.gameObject.name.Contains("pinky_2"))
                        {
                            grabbingController.isPinkyTouchingCube = false;
                            //pinkyFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                        }
                    }
                }

                foreach (ContactPoint pointToDelete in contactPointsToDelete)
                {
                    if (lastContactPoints.Contains(pointToDelete))
                    {
                        lastContactPoints.Remove(pointToDelete);
                    }
                }
                contactPointsToDelete.Clear();

                if (!(grabbingController.isThumbTouchingCube || grabbingController.isIndexTouchingCube || grabbingController.isMiddleTouchingCube || grabbingController.isRingTouchingCube || grabbingController.isPinkyTouchingCube))
                {
                    grabbingController.grabbedObject = null;
                }
            }
        }
    }
}
