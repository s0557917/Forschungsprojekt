using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace VrPassing.Utilities
{
    public static class TimeManager
    {
        public static IEnumerator StartCountdown(UnityEvent countdownHasFinished, float countdownDuration = 60f)
        {
            float totalTime = 0;

            while (totalTime <= countdownDuration)
            {
                totalTime += Time.deltaTime;
                yield return null;
            }
            countdownHasFinished.Invoke();
        }
    }
}
