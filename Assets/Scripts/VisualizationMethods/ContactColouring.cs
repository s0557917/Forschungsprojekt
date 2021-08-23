using UnityEngine;

namespace VrPassing.ContactVisualizationTools
{
    public class ContactColouring : MonoBehaviour, IVisualizationMethod
    {
        private Renderer _objectRenderer;
        private Material _objectMaterial;
        private Shader _objectShader;

        private Material _highlightMaterial;

        private void Start()
        {
            _objectRenderer = this.GetComponent<Renderer>();
            _objectShader = _objectRenderer.material.shader;
            _objectMaterial = _objectRenderer.material;
            _highlightMaterial = Resources.Load<Material>("Materials/Contact Visualization Materials/ContactHighlighterMaterial");
        }

        private void OnCollisionEnter(Collision collision)
        {
            _objectRenderer.material = _highlightMaterial;
        }

        private void OnCollisionExit(Collision collision)
        {
            _objectRenderer.material = _objectMaterial;
        }

        public void RemoveVisualization()
        {
            _objectRenderer.material = _objectMaterial;
            Destroy(this);
        }
    }
}