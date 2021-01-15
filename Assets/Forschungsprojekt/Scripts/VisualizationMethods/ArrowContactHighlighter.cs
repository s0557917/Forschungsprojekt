using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContactHighlighter : MonoBehaviour
{
    public ContactPoint[] limitedContactPoints = new ContactPoint[10];
    ContactPoint[] contactPoints = new ContactPoint[10];

    List<GameObject> arrowList = new List<GameObject>();

    Material cubeMaterial;

    float collisionForce;

    GameObject arrowPrefab;

    private void Start()
    {
        cubeMaterial = Resources.Load<Material>("Materials/CubeMaterial");
        arrowPrefab = Resources.Load<GameObject>("Prefabs/Arrow");
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionForce = collision.relativeVelocity.magnitude;

        if (collision.gameObject.CompareTag("VRController"))
        {
            contactPoints = collision.contacts;

            if (limitedContactPoints.Length > contactPoints.Length)
            {
                for (int i = 0; i < contactPoints.Length; i++)
                {
                    limitedContactPoints[i] = contactPoints[i];
                }
            }
            else
            {
                for (int i = 0; i < limitedContactPoints.Length; i++)
                {
                    limitedContactPoints[i] = contactPoints[i];
                }
            }

            foreach (ContactPoint contactPoint in limitedContactPoints)
            {
                Debug.DrawLine(this.transform.position, contactPoint.normal, Color.red);

                //GameObject newArrow = GameObject.Instantiate(arrowPrefab);
                //newArrow.transform.parent = this.transform;
                //newArrow.transform.position = contactPoint.point;
                //newArrow.transform.rotation = Quaternion.FromToRotation(newArrow.transform.eulerAngles, contactPoint.normal);
                ////Vector3 newScale = newArrow.transform.localScale;
                ////newScale.y = 1;
                //newArrow.transform.localScale = new Vector3(newArrow.transform.localScale.x,5, newArrow.transform.localScale.z);
                //arrowList.Add(newArrow);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("VRController"))
        {
            contactPoints = collision.contacts;

            //for (int i = 0; i < limitedContactPoints.Length; i++)
            //{
            //    limitedContactPoints[i] = contactPoints[i];
            //}

            //foreach (GameObject arrow in arrowList)
            //{
            //    Destroy(arrow);

            //}

            //arrowList.Clear();

            foreach (ContactPoint contactPoint in limitedContactPoints)
            {
                Debug.DrawLine(this.transform.position, contactPoint.normal, Color.red);

                //GameObject newArrow = GameObject.Instantiate(arrowPrefab);
                //newArrow.transform.parent = this.transform;
                //newArrow.transform.position = contactPoint.point;
                //newArrow.transform.rotation = Quaternion.FromToRotation(newArrow.transform.eulerAngles, contactPoint.normal);
                //Vector3 newScale = newArrow.transform.localScale;
                //newScale.y = 1;
                //newArrow.transform.localScale = newScale;
                //arrowList.Add(newArrow);
            }
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("VRController"))
    //    {
    //        foreach (GameObject arrow in arrowList)
    //        {
    //            Destroy(arrow);

    //        }

    //        arrowList.Clear();
    //    }
    //}
}
