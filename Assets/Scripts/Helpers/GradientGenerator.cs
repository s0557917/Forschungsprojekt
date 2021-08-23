using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VrPassing.Utilities
{
    public static class GradientGenerator
    {
        public static Gradient Generate(int keyCount, Color[] colors)
        {
            Gradient gradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[keyCount];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[keyCount];

            float timeStep = 1 / (keyCount - 1);
            float currentTime = 0;

            for (int i = 0; i < keyCount; i++)
            {
                colorKeys[i].color = colors[i];
                colorKeys[i].time = currentTime;
                alphaKeys[i].alpha = 1f;
                alphaKeys[i].time = currentTime;

                currentTime += timeStep;
            }

            gradient.SetKeys(colorKeys, alphaKeys);

            return gradient;
        }
    }
}
