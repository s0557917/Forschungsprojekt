using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_DrawFingerRays : MonoBehaviour
{
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.yellow);    
    }
}
