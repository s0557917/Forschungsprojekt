using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VrPassing.Events;

public class FingerCollisionDetection : MonoBehaviour
{
    public enum Finger
    {
        Thumb,
        Index,
        Middle,
        Ring,
        Pinky
    }

    [SerializeField] private Finger attachedFinger;
    [SerializeField] private int cubeLayer = 11;

    private FingerInteractionNotification fingerEvent;

    private void Awake()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == cubeLayer)
        {
            switch (attachedFinger)
            {
                case Finger.Thumb:
                    Debug.Log("-- THUMB");
                    break;
                case Finger.Index:
                    Debug.Log("-- INDEX");
                    break;
                case Finger.Middle:
                    Debug.Log("-- MIDDLE");
                    break;
                case Finger.Ring:
                    Debug.Log("-- RING");
                    break;
                case Finger.Pinky:
                    Debug.Log("-- PINKY");
                    break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

}
