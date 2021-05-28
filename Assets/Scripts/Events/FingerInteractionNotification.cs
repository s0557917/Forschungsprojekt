using System;
using UnityEngine;
using UnityEngine.Events;

namespace VrPassing.Events
{
    [Serializable]
    public class FingerInteractionNotification : UnityEvent<bool, GameObject> { }
}
