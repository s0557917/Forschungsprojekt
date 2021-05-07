using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResizer : MonoBehaviour
{
    public BoxCollider boxCollider;
    public RectTransform rectTransform;
    public float width;
    public float height;

    public Vector3 colliderSize;

    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        width = rectTransform.sizeDelta.x;
        height = rectTransform.sizeDelta.y;

        boxCollider = this.GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(1f, 0.5f, 0.1f);
        colliderSize = boxCollider.size;
    }

    //private void Update()
    //{
    //    width = rectTransform.sizeDelta.x;
    //    height = rectTransform.sizeDelta.y;


    //    colliderSize = boxCollider.size;
    //    boxCollider.size = new Vector3(width, height, boxCollider.size.z);

    //}
}
