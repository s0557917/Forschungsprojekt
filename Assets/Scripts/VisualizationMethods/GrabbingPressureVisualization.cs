using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrabbingPressureVisualization : MonoBehaviour
{
    public Gradient pressureVisualizationGradient;

    Renderer objectRenderer;
    Material objectMaterial;
    Material pressureColorizationMaterial;

    // Grip
    SteamVR_Action_Single gripSqueeze = SteamVR_Input.GetAction<SteamVR_Action_Single>("Squeeze");
    float gripStrength;
    Interactable interactable;

    // a reference to the hand
    public SteamVR_Input_Sources handType;
                
    void Start()
    {
        objectRenderer = this.GetComponent<Renderer>();
        objectMaterial = objectRenderer.material;

        pressureColorizationMaterial = Resources.Load<Material>("Materials/Contact Visualization Materials/PressureHighlightMaterial");

        objectRenderer.material = pressureColorizationMaterial;

        interactable = this.GetComponent<Interactable>();
    }

    void Update()
    {
        gripStrength = 0;

        if (interactable.attachedToHand)
        {
            gripStrength = gripSqueeze.GetAxis(handType);
        }

        Color correspondingGradientColor = pressureVisualizationGradient.Evaluate(gripStrength);

        //pressureColorizationMaterial.SetFloat("Vector1_D9FE816E", gripStrength);
        pressureColorizationMaterial.SetColor("Color_28EF2E3E", correspondingGradientColor);

    }

    void onDisable()
    {
        objectRenderer.material = objectMaterial;
    }

}
