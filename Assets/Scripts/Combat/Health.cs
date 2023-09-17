using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _healthPoints = 100f;
        
        private Animator _animator;
        
        private static int AP_DEATH_TRIGGER = Animator.StringToHash("die");
        private bool _isDead = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            Debug.Log(_healthPoints);
            if (_healthPoints == 0 && !_isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            _animator.SetTrigger(AP_DEATH_TRIGGER);
        }
    }
}