using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VrPassing.ContactVisualizationTools
{
    public class PressureArrows : MonoBehaviour, IVisualizationMethod
    {
        [Range(0, 1)]
        public float gripStrength = 0;
        private ContactPoint[] _contactPoints;
        private ContactPoint[] _limitedContactPoints = new ContactPoint[5];
        private List<GameObject> _arrowList = new List<GameObject>();
        private GameObject _arrowPrefab;
        private Material _cubeMaterial;

        private void Start()
        {
            _cubeMaterial = Resources.Load<Material>("Materials/CubeMaterial");
            _arrowPrefab = Resources.Load<GameObject>("Prefabs/InteractionVisualization/ArrowPlaceholder");
        }

        private void OnCollisionEnter(Collision collision)
        {

            GenerateContactArrows(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            ClearContactArrows();
            GenerateContactArrows(collision);
        }

        private void GenerateContactArrows(Collision collision)
        {
            _contactPoints = collision.contacts;

            if (_limitedContactPoints.Length > _contactPoints.Length)
            {
                for (int i = 0; i < _contactPoints.Length; i++)
                {
                    _limitedContactPoints[i] = _contactPoints[i];
                }
            }
            else
            {
                for (int i = 0; i < _limitedContactPoints.Length; i++)
                {
                    _limitedContactPoints[i] = _contactPoints[i];
                }
            }

            foreach (ContactPoint contactPoint in _limitedContactPoints)
            {
                GameObject newArrow = CreateNewArrow(contactPoint);
                _arrowList.Add(newArrow);
            }
        }

        private GameObject CreateNewArrow(ContactPoint contactPoint)
        {
            GameObject newArrow = GameObject.Instantiate(_arrowPrefab);
            newArrow.transform.parent = this.transform;
            newArrow.transform.position = contactPoint.point;
            newArrow.transform.rotation = Quaternion.FromToRotation(newArrow.transform.eulerAngles, contactPoint.normal);
            newArrow.transform.localScale = new Vector3(newArrow.transform.localScale.x, gripStrength, newArrow.transform.localScale.z);
            return newArrow;
        }

        private void ClearContactArrows()
        {
            foreach (GameObject arrow in _arrowList)
            {
                Destroy(arrow);
            }

            _arrowList.Clear();
        }

        private void OnCollisionExit(Collision collision)
        {
            ClearContactArrows();
        }

        public void RemoveVisualization()
        {
            ClearContactArrows();
            Destroy(this);
        }
    }
}
