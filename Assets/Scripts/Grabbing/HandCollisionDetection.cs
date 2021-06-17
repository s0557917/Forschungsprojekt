using System.Text;
using UnityEngine;
using UnityEngine.Events;
using VrPassing.Events;

namespace VrPassing.Grabbing
{
    public class HandCollisionDetection : MonoBehaviour
    {
        [SerializeField] private int cubeLayer = 11;

        private FingerInteractionNotification thumbFingerEvent;
        private FingerInteractionNotification indexFingerEvent;
        private FingerInteractionNotification middleFingerEvent;
        private FingerInteractionNotification ringFingerEvent;
        private FingerInteractionNotification pinkyFingerEvent;

        private ContactPoint[] lastContactPoints;

        public void SetupFingerEvents(FingerInteractionNotification thumbFingerEvent, 
            FingerInteractionNotification indexFingerEvent,
            FingerInteractionNotification middleFingerEvent,
            FingerInteractionNotification ringFingerEvent,
            FingerInteractionNotification pinkyFingerEvent
        )
        {
            this.thumbFingerEvent = thumbFingerEvent;
            this.indexFingerEvent = indexFingerEvent;
            this.middleFingerEvent = middleFingerEvent;
            this.ringFingerEvent = ringFingerEvent;
            this.pinkyFingerEvent = pinkyFingerEvent;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.layer == cubeLayer)
            {
                lastContactPoints = collision.contacts;

                foreach (ContactPoint contact in collision.contacts)
                {
                    if (contact.thisCollider.gameObject.name.Contains("thumb"))
                    {
                        thumbFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("index_2"))
                    {
                        indexFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("middle_2"))
                    {
                        middleFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("ring_2"))
                    {
                        ringFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("pinky_2"))
                    {
                        pinkyFingerEvent.Invoke(true, contact.otherCollider.gameObject);
                    }
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == cubeLayer && collision.contacts.Length > 0)
            {
                //Debug.Log("COLLIDING");
                StringBuilder sb = new StringBuilder();
                sb.Append("++++++ OnStay:: ");
                foreach (var item in collision.contacts)
                {
                    sb.Append("(" + item.thisCollider.name + "||" + item.otherCollider.name + ") - ");
                }

                Debug.Log(sb.ToString());
                lastContactPoints = collision.contacts;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.layer == cubeLayer)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("----- OnExit:: ");
                foreach (var item in lastContactPoints)
                {
                    sb.Append("(" + item.thisCollider.name + "||" + item.otherCollider.name + ") - ");
                }

                Debug.Log(sb.ToString());
                foreach (ContactPoint contact in lastContactPoints)
                {
                    if (contact.thisCollider.gameObject.name.Contains("thumb"))
                    {
                        thumbFingerEvent.Invoke(false, null);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("index_2"))
                    {
                        indexFingerEvent.Invoke(false, null);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("middle_2"))
                    {
                        middleFingerEvent.Invoke(false, null);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("ring_2"))
                    {
                        ringFingerEvent.Invoke(false, null);
                    }
                    else if (contact.thisCollider.gameObject.name.Contains("pinky_2"))
                    {
                        pinkyFingerEvent.Invoke(false, null);
                    }
                }
            }

        }
    }
}
