using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        public bool IsDead { get; private set; }
        
        [SerializeField] private float _healthPoints = 100f;
        
        private ActionScheduler _actionScheduler;
        
        private Animator _animator;

        private static int AP_DEATH_TRIGGER = Animator.StringToHash("die");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            Debug.Log(name + " | Health Points: " + _healthPoints);
            if (_healthPoints == 0 && !IsDead)
            {
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
            _animator.SetTrigger(AP_DEATH_TRIGGER);
            _actionScheduler.CancelCurrentAction();
        }

        public object CaptureState()
        {
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float) state;
            if (_healthPoints == 0 && !IsDead)
            {
                Die();
            }
        }
    }
}