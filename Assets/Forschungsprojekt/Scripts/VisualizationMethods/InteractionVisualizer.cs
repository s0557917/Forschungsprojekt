using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace ContactVisualizationTools
{
    [RequireComponent(typeof(ContactHighlighter))]
    [RequireComponent(typeof(GrabbingPressureVisualization))]
    [RequireComponent(typeof(ContactHeatmap))]
    public class InteractionVisualizer : MonoBehaviour
    {
        Renderer objectRenderer;
        Material objectMaterial;
        Color newColor;

        ContactHighlighter contactHighlighter;
        GrabbingPressureVisualization pressureHighlighter;
        ContactHeatmap contactHeatmap;

        //Object Color Change
        public Material highlightMaterials;
        Material[] originalMaterials;

        int lastState = 0;

        void Start()
        {
            contactHighlighter = this.GetComponent<ContactHighlighter>();
            contactHighlighter.enabled = false;
            pressureHighlighter = this.GetComponent<GrabbingPressureVisualization>();
            pressureHighlighter.enabled = false;
            contactHeatmap = this.GetComponent<ContactHeatmap>();
            contactHeatmap.enabled = false;

        }

        void ContactColorizationHandler()
        {
            if (lastState != 1)
            {
                contactHighlighter.enabled = true;
                pressureHighlighter.enabled = false;
                contactHeatmap.enabled = false;
                lastState = 1;
            }

        }

        void PressureColorizationHandler()
        {
            if (lastState != 2)
            {
                contactHighlighter.enabled = false;
                pressureHighlighter.enabled = true;
                contactHeatmap.enabled = false;
                lastState = 2;
            }
        }

        void ContactHeatmapSelected()
        {
            if (lastState != 3)
            {
                contactHighlighter.enabled = false;
                pressureHighlighter.enabled = false;
                contactHeatmap.enabled = true;

                lastState = 3;
            }
        }

        private void OnEnable()
        {
            VisualizationEvents.onContactColorizationSelected.AddListener(ContactColorizationHandler);
            VisualizationEvents.onPressureColorizationSelected.AddListener(PressureColorizationHandler);
            VisualizationEvents.onContactHeatmapSelected.AddListener(ContactHeatmapSelected);
        }

        private void OnDisable()
        {

        }

        //private void OnCollisionEnter(Collision collision)
        //{

        //    if (collision.gameObject.CompareTag("VRController"))
        //    {
        //        objectRenderer.material.color = Color.red;
        //    }
        //}

        //private void OnCollisionExit(Collision collision)
        //{

        //    if (objectRenderer.materials != originalMaterials)
        //    {
        //        objectRenderer.materials = originalMaterials;
        //    }
        //}
    }

}