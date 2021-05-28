using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TEST_GraphicRaycaster : MonoBehaviour
{
    [SerializeField] GameObject controller;

    GraphicRaycaster gr;
    PointerEventData ped;
    EventSystem evs;
 
    // Start is called before the first frame update
    void Start()
    {
        gr = this.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
        evs = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        //Set up the new Pointer Event
        ped = new PointerEventData(evs);
        //Set the Pointer Event Position to that of the game object
        ped.position = controller.transform.position;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        gr.Raycast(ped, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
        else
        {
            Debug.Log("No raycast hits");
        }
    }
}
