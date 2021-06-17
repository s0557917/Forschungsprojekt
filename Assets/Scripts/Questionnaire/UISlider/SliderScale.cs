using UnityEngine.UI;
using UnityEngine;
using VrPassing.Events;

//TODO - REMOVE
public class SliderScale : MonoBehaviour
{
    public float slideValue = 0.1f;
    public Slider slider;

    [HideInInspector] public UpdateSliderEvent updateSliderEvent;

    private void OnEnable()
    {
        updateSliderEvent = new UpdateSliderEvent();
        updateSliderEvent.AddListener(UpdateSliderValue);
    }

    private void UpdateSliderValue(string buttonAction)
    {
        if (buttonAction == SliderButton.Action.Decrease.ToString())
        {
            slider.value -= slideValue;
        }
        else if (buttonAction == SliderButton.Action.Increase.ToString())
        {
            slider.value += slideValue;

        }
    }

    private void OnDisable()
    {
        if (updateSliderEvent != null)
        {
            updateSliderEvent.RemoveAllListeners();
        }
    }
}
