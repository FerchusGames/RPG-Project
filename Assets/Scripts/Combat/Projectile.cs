using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private Transform _target;

        private void Update()
        {
            if (_target == null)
                return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider capsuleCollider = _target.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                return _target.position;
            }
            return _target.position + Vector3.up * capsuleCollider.height / 2;
        }
    }
}
