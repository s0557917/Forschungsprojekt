using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderResizer : MonoBehaviour
{
    [SerializeField] GameObject colliderCube;
    [SerializeField] RectTransform buttonRectTransform;

    float buttonWidth;
    float buttonHeight;

    private void Start()
    {
        resizeColliderCube();
    }

    private void OnRectTransformDimensionsChange()
    {
        resizeColliderCube();
    }

    private void resizeColliderCube()
    {
        buttonWidth = buttonRectTransform.rect.width;
        buttonHeight = buttonRectTransform.rect.height;

        colliderCube.transform.localScale = new Vector3(buttonWidth, buttonHeight, 0.1f);
    }
}
