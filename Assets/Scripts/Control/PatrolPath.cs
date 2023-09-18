using UnityEditor;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float _waypointGizmoRadius = 0.3f;
        
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(transform.GetChild(i).position, _waypointGizmoRadius);
            }
        }
    }
}
