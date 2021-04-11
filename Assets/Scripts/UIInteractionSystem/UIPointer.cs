using UnityEngine;
using UnityEngine.UI;

public class UIPointer : MonoBehaviour
{
    [SerializeField]
    private float defaultLength = 10;

    [SerializeField]
    Material hitLineMaterial;
    [SerializeField]
    Material noHitLineMaterial;

    float startLineWidth = 0.01f;
    float endLineWidth = 0.001f;
    LineRenderer lineRenderer;

    int combinedAndInvertedLayerMasks = ~((1 << 8) | (1 << 9));

    Vector3 endPosition;
    [SerializeField]
    GameObject pointerDot;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null){
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

        if (hit.collider != null )
        {
            
            Button selectedButton = hit.collider.GetComponent<Button>();

            if (selectedButton != null )
            {
                endPosition = hit.point;
                lineRenderer.material = hitLineMaterial;
                selectedButton.transition = Selectable.Transition.ColorTint;

                if (Input.GetKeyDown(KeyCode.T))
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
        else
        {
            endPosition = transform.position + (transform.forward * targetLength);
            lineRenderer.material = noHitLineMaterial;
        }

        pointerDot.transform.position = endPosition;

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
