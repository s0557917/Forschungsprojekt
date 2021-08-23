using System;
using UnityEngine;
using UnityEngine.Events;

namespace VrPassing.Events
{
    [Serializable]
    public class FingerInteractionEvent : UnityEvent<bool, GameObject> { }
}
