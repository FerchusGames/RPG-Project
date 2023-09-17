using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        void LateUpdate()
        {
            this.transform.position = _target.position;
        }
    }
}