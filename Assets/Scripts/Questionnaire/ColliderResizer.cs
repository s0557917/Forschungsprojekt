using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderResizer : MonoBehaviour
{
    [SerializeField] GameObject colliderCube;
    [SerializeField] RectTransform rectTransform;

    float width;
    float height;

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
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        colliderCube.transform.localScale = new Vector3(width, height, 0.1f);
    }
}
