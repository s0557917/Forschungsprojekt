using UnityEngine;
using VrPassing.Utilities;

namespace VrPassing.ContactVisualizationTools
{
    public class PressureColouring : MonoBehaviour, IVisualizationMethod
    {
        [Range(0, 1)]
        public float gripStrength = 0;
        public Gradient pressureVisualizationGradient;
        
        private Renderer _objectRenderer;
        private Material _objectMaterial;
        private Material _pressureColorizationMaterial;
        private Color _correspondingGradientColor;

        private bool _isColliding = false;

        public void RemoveVisualization()
        {
            _objectRenderer.material = _objectMaterial;
            Destroy(this);
        }

        private void Start()
        {
            _objectRenderer = this.GetComponent<Renderer>();
            _objectMaterial = _objectRenderer.material;
            _pressureColorizationMaterial = Resources.Load<Material>("Materials/Contact Visualization Materials/PressureHighlightMaterial");
            _objectRenderer.material = _pressureColorizationMaterial;

            pressureVisualizationGradient = GradientGenerator.Generate(2, new Color[] { Color.blue, Color.red });
        }

        private void Update()
        {
            if (_isColliding)
            {
                _correspondingGradientColor = pressureVisualizationGradient.Evaluate(gripStrength);
                _pressureColorizationMaterial.SetColor("Color_28EF2E3E", _correspondingGradientColor);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            _isColliding = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            _isColliding = false;
        }

        private void onDisable()
        {
            _objectRenderer.material = _objectMaterial;
        }
    }
}
