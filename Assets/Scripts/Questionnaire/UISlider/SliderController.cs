using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

namespace VrPassing.UIInteraction
{
    public class SliderController : MonoBehaviour
    {

        [HideInInspector] public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "InteractUI");
        [HideInInspector] public bool wasHandleSelected;
        [HideInInspector] public Vector3 startingPosition;
        [HideInInspector] public GameObject clickingController;

        [SerializeField] private float threshold = 0.01f;
        private bool isClicking;
        private Slider attachedSlider;

        void Awake()
        {
            attachedSlider = this.GetComponent<Slider>();
            interactWithUI.onStateDown += delegate { isClicking = true; };
            interactWithUI.onState += delegate { isClicking = true; };
            interactWithUI.onStateUp += delegate {
                isClicking = false;
                wasHandleSelected = false;
            };
        }

        void Update()
        {
            if (isClicking && wasHandleSelected)
            {
                float distance = startingPosition.x - clickingController.transform.position.x;

                if (clickingController.transform.position.x > startingPosition.x + threshold)
                {
                    attachedSlider.value += 0.005f;
                }
                else if (clickingController.transform.position.x < startingPosition.x - threshold)
                {
                    attachedSlider.value -= 0.005f;
                }
            }
        }
    }
}
