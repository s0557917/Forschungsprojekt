using UnityEngine;
using UnityEngine.UI;

public class SliderButton : MonoBehaviour
{
    public enum Action
    {
        Increase, 
        Decrease
    }

    public Action buttonAction;
    public SliderScale owner;

    private void Start()
    {
        this.GetComponent<Button>()?.onClick.AddListener(
            () => owner.updateSliderEvent.Invoke(buttonAction.ToString()));
    }
}
