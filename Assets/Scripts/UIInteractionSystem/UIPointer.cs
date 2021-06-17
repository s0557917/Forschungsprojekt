using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

namespace VrPassing.UIInteraction
{
    public class UIPointer : MonoBehaviour
    {
        [SerializeField] private float defaultLength = 10;
        [SerializeField] private Material hitLineMaterial;
        [SerializeField] private Material noHitLineMaterial;
        [SerializeField] private GameObject pointerDot;

        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "InteractUI");

        private float startLineWidth = 0.01f;
        private float endLineWidth = 0.001f;
        private LineRenderer lineRenderer;

        private int combinedAndInvertedLayerMasks = ~((1 << 8) | (1 << 9));

        private Vector3 endPosition;
        private SliderController sliderController;

        void Start()
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }

            lineRenderer.startWidth = startLineWidth;
            lineRenderer.endWidth = endLineWidth;
            lineRenderer.material = noHitLineMaterial;
        }

        private void FixedUpdate()
        {
            UpdateLine();
        }

        private void UpdateLine()
        {
            float targetLength = defaultLength;
            RaycastHit hit = CreateRaycast();
            Vector3 endPosition = transform.position + (transform.forward * targetLength);

            if (hit.collider != null)
            {
                endPosition = hit.point;

                try
                {
                    switch (hit.collider.gameObject.tag)
                    {
                        case "PartitionButton":
                        case "PaginationButton":

                            Button selectedButton = hit.collider.transform?.parent.GetComponent<Button>();
                            if (selectedButton != null)
                            {
                                selectedButton.Select();
                                endPosition = hit.point;
                                lineRenderer.material = hitLineMaterial;

                                if (interactWithUI.stateDown)
                                {
                                    selectedButton.onClick.Invoke();
                                }
                            }

                            break;
                        case "SliderHandle":

                            Slider selectedSlider = hit.collider.transform?.parent.transform.parent.transform.parent.GetComponent<Slider>();
                            if (selectedSlider != null)
                            {
                                selectedSlider.Select();
                                sliderController = selectedSlider.GetComponent<SliderController>();
                                endPosition = hit.point;
                                lineRenderer.material = hitLineMaterial;

                                if (interactWithUI.state && !sliderController.wasHandleSelected)
                                {
                                    sliderController.wasHandleSelected = true;
                                    sliderController.clickingController = this.transform.parent.gameObject;
                                    Debug.Log("STARTING POSITION CHANGED");
                                    sliderController.startingPosition = this.transform.position;
                                }
                            }

                            break;
                        default:

                            endPosition = transform.position + (transform.forward * targetLength);
                            lineRenderer.material = noHitLineMaterial;

                            break;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            else
            {
                endPosition = transform.position + (transform.forward * targetLength);
                lineRenderer.material = noHitLineMaterial;
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition);

        }

        private RaycastHit CreateRaycast()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, defaultLength, combinedAndInvertedLayerMasks);

            return hit;
        }
    }
}
