using System;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;

        float _damage = 0f;

        private Health _target;

        private void Update()
        {
            if (_target == null)
                return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        public void SetTarget(Health target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider capsuleCollider = _target.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                return _target.transform.position;
            }
            return _target.transform.position + Vector3.up * capsuleCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target)
                return;
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
