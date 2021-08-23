using System;
using UnityEngine;
using VrPassing.ContactVisualizationTools;

public class VisualizationMethodSwitcher : MonoBehaviour
{
    InteractionVisualizer vis;
    public int currentVis = 0;
    public Rigidbody rigid;

    void Start()
    {
        vis = gameObject.AddComponent<InteractionVisualizer>();
        vis.SetInteractionVisualization((InteractionVisualizer.Visualizations)currentVis);
        rigid = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentVis ++;
            vis.SetInteractionVisualization((InteractionVisualizer.Visualizations)currentVis);
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigid.AddForce(Vector3.up * 1000, ForceMode.Acceleration);
        }
    }
}
