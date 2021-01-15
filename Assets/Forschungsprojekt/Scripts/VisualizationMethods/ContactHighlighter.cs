using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHighlighter : MonoBehaviour
{
    Renderer objectRenderer;
    Shader objectShader;

    Material highlightMaterial;

    private void Start()
    {
        objectRenderer = this.GetComponent<Renderer>();
        objectShader = objectRenderer.material.shader;

        highlightMaterial = Resources.Load<Material>("Materials/Contact Visualization Materials/ContactHighlighterMaterial");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("VRController"))
        {
            Debug.Log("Material Set");  
            objectRenderer.material = highlightMaterial;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.CompareTag("VRController"))
        //{
        //    objectRenderer.material = highlightMaterial;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("VRController"))
        {
            Debug.Log("Material removed");
            objectRenderer.material.shader = objectShader;
        }
    }
}
