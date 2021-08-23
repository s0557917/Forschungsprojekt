using UnityEngine;

namespace VrPassing.ContactVisualizationTools
{
    public class InteractionVisualizer : MonoBehaviour
    {
        private IVisualizationMethod addedVisMethod;

        public enum Visualizations
        {
            ContactColouring,
            PressureColouring, 
            ContactArrows, 
            PressureArrows, 
            ContactHeatmap
        }

        public void SetInteractionVisualization(Visualizations visualization)
        {
            switch (visualization)
            {
                case Visualizations.ContactColouring:
                    RemoveLastVisMethod();
                    addedVisMethod = gameObject.AddComponent<ContactColouring>();
                    break;
                case Visualizations.PressureColouring:
                    RemoveLastVisMethod();
                    addedVisMethod = gameObject.AddComponent<PressureColouring>();
                    break;
                case Visualizations.ContactArrows:
                    RemoveLastVisMethod();
                    addedVisMethod = gameObject.AddComponent<ContactArrows>();
                    break;
                case Visualizations.PressureArrows:
                    RemoveLastVisMethod();
                    addedVisMethod = gameObject.AddComponent<PressureArrows>();
                    break;
                case Visualizations.ContactHeatmap:
                    RemoveLastVisMethod();
                    addedVisMethod = gameObject.AddComponent<ContactHeatmap>();
                    break;
                default:
                    Debug.LogError("A non valid visualization method was passed!");
                    break;
            }
        }

        private void RemoveLastVisMethod()
        {
            if (addedVisMethod != null)
            {
                addedVisMethod.RemoveVisualization();
            }
        }
    }

}