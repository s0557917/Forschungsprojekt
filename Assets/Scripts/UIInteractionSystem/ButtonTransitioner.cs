using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 normalColor = Color.white;
    public Color32 hoverColor = Color.grey;
    public Color32 downColor = Color.white;

    private RawImage buttonImage = null;

    private void Awake()
    {
        buttonImage = GetComponent<RawImage>();
        normalColor = buttonImage.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = downColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
