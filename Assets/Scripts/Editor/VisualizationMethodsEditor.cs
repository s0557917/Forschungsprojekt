using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ContactVisualizationTools
{
    [CustomEditor(typeof(InteractionVisualizer))]
    public class VisualizationMethodsEditor : Editor
    {
        //public delegate void ContactColorizationSelected();
        //public static event ContactColorizationSelected onContactColorizationSelected;

        //public delegate void PressureColorizationSelected();
        //public static event PressureColorizationSelected onPressureColorizationSelected;

        //public delegate void ContactHeatmapSelected();
        //public static event ContactHeatmapSelected onContactHeatmapSelected;

        [SerializeField]
        public enum VisualisationMethods
        {
            ContactColorization,
            PressureColorization,
            ContactHeatmap

        }

        public VisualisationMethods visualizationDropdown;


        private void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();


            visualizationDropdown = (VisualisationMethods)EditorGUILayout.EnumPopup("Contact Visualization Method", visualizationDropdown);

            //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            switch (visualizationDropdown)
            {
                case VisualisationMethods.ContactColorization:
                    //EditorGUILayout.ColorField("Contact Highlight Color: ", Color.white);
                    VisualizationEvents.onContactColorizationSelected.Invoke();
                    break;
                case VisualisationMethods.PressureColorization:
                    //EditorGUILayout.ColorField("Pressure Display Color", Color.red);
                    VisualizationEvents.onPressureColorizationSelected.Invoke();
                    break;
                case VisualisationMethods.ContactHeatmap:
                    //EditorGUILayout.LabelField("Heatmap color gradient");
                    //EditorGUILayout.ColorField("High intensity", Color.red);
                    //EditorGUILayout.ColorField("Middle intensity", Color.blue);
                    //EditorGUILayout.ColorField("Low intensity", Color.green);
                    VisualizationEvents.onContactHeatmapSelected.Invoke();
                    break;
                default:

                    break;
            }

            //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            //EditorGUILayout.BeginToggleGroup("Visualization Target", true);
            //EditorGUILayout.Toggle("Object", true);
            //EditorGUILayout.Toggle("Hand", false);
            //EditorGUILayout.EndToggleGroup();
        }
    }
}