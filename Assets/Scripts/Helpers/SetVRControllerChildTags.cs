using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VrPassing.Utilities
{
    public class SetVRControllerChildTags : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //SetTagOfAllChildren(this.transform);
            //foreach (Transform child in this.transform)
            //{
            //    child.tag = "VRController";
            //}

            //GameObject controllerRight = transform.GetChild(6).gameObject;
            //GameObject controllerLeft = transform.GetChild(7).gameObject;

            //Debug.Log("## LEFT: " + controllerLeft.name + " - RIGHT: " + controllerRight.name);

            //controllerRight.tag = "VRController";
            //controllerRight.tag = "VRController";
        }


        //private void SetTagOfAllChildren(Transform parent)
        //{
        //    foreach (Transform child in parent)
        //    {
        //        child.gameObject.tag = "VRController";
        //        SetTagOfAllChildren(child);
        //    }
        //}

        private void OnTransformChildrenChanged()
        {
            foreach (Transform child in this.transform)
            {
                if (child.name.Equals("HandColliderLeft(Clone)") || child.name.Equals("HandColliderRight(Clone)"))
                {
                    child.tag = "VRController";
                }
            }
        }
    }
}
