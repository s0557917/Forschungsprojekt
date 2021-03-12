using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1000, out hit, Mathf.Infinity))
        //{
        //    Debug.Log("Hit :: " + hit.collider.name);

        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    lineRenderer.SetPositions(new Vector3[] { transform.position, hit.point});
        //    lineRenderer.material = hitLineMaterial;
        //}
        //else
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        //    lineRenderer.SetPositions(new Vector3[] { transform.position, transform.TransformDirection(Vector3.forward) * 100});
        //}

        UpdateLine();
    }

    private void UpdateLine()
    {
        float targetLength = defaultLength;
        RaycastHit hit = CreateRaycast();

        if (hit.collider != null && ( hit.collider.gameObject.layer != 8 || hit.collider.gameObject.layer != 9))
        {
            Debug.Log("Colliding :::: " + hit.collider.name);

            endPosition = hit.point;
            lineRenderer.material = hitLineMaterial;
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

  //  // Example: get controller's current orientation:
  //  Quaternion ori = GvrController.Orientation;

  //  // If you want a vector that points in the direction of the controller
  //  // you can just multiply this quat by Vector3.forward:
  //  Vector3 vector = ori * Vector3.forward;

  //  // ...or you can just change the rotation of some entity on your scene
  //  // (e.g. the player's arm) to match the controller's orientation
  //  playerArmObject.transform.localRotation = ori;

  //// Example: check if touchpad was just touched
  //if (GvrController.TouchDown) {
  //  // Do something.
  //  // TouchDown is true for 1 frame after touchpad is touched.

  //  PointerEventData pointerData = new PointerEventData(EventSystem.current);

  //  pointerData.position = Input.mousePosition; // use the position from controller as start of raycast instead of mousePosition.

  //  List<RaycastResult> results = new List<RaycastResult>();
  //  EventSystem.current.RaycastAll(pointerData, results);

  //  if (results.Count > 0) {
  //       //WorldUI is my layer name
  //       if (results[0].gameObject.layer == LayerMask.NameToLayer("WorldUI")){ 
  //       string dbg = "Root Element: {0} \n GrandChild Element: {1}";
  //  Debug.Log(string.Format(dbg, results[results.Count - 1].gameObject.name, results[0].gameObject.name));
  //       //Debug.Log("Root Element: "+results[results.Count-1].gameObject.name);
  //       //Debug.Log("GrandChild Element: "+results[0].gameObject.name);
  //        results.Clear();
  //   }
  // }
//}
}
