using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ContactVisualizationTools
{
    public class VisualizationEvents
    {
        public static UnityEvent onContactColorizationSelected = new UnityEvent();
        public static UnityEvent onContactHeatmapSelected = new UnityEvent();
        public static UnityEvent onPressureColorizationSelected = new UnityEvent();
    }    
}