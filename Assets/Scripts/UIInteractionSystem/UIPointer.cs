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

        float startLineWidth = 0.01f;
        float endLineWidth = 0.001f;
        LineRenderer lineRenderer;

        int combinedAndInvertedLayerMasks = ~((1 << 8) | (1 << 9));

        Vector3 endPosition;

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
                    Button selectedButton = hit.collider.transform?.parent.GetComponent<Button>();

                    if (selectedButton != null)
                    {
                        selectedButton.Select();
                        endPosition = hit.point;
                        lineRenderer.material = hitLineMaterial;

                        if ((selectedButton.CompareTag("PartitionButton") || selectedButton.CompareTag("PaginationButton")) && interactWithUI.stateDown)
                        {
                            selectedButton.onClick.Invoke();
                        }
                        else if (selectedButton.CompareTag("SliderButton") && interactWithUI.state)
                        {
                            selectedButton.onClick.Invoke();
                        }
                    }
                    else
                    {
                        endPosition = transform.position + (transform.forward * targetLength);
                        lineRenderer.material = noHitLineMaterial;
                    }
                }
                catch (System.Exception)
                {

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
