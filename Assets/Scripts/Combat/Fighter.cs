using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _weaponDamage = 5f;
        
        private ActionScheduler _actionScheduler;

        private Animator _animator;
        private Health _combatTarget;
        private Mover _mover;
        
        private static int AP_ATTACK_TRIGGER = Animator.StringToHash("attack");
        private static int AP_STOP_ATTACK_TRIGGER = Animator.StringToHash("stopAttack");
        
        private float _timeSinceLastAttack = 0;
        
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_combatTarget == null) return;
            if (_combatTarget.IsDead) return;
            
            if (!IsInRange())
            {
                _mover.MoveTo(_combatTarget.transform.position);
            }
            
            else
            {
                _mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(_combatTarget.transform);
            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                // This will trigger the Hit() animation event.
                _animator.SetTrigger(AP_ATTACK_TRIGGER);
                _timeSinceLastAttack = 0;
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _combatTarget.transform.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _combatTarget = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            _animator.SetTrigger(AP_STOP_ATTACK_TRIGGER);
            _combatTarget = null;
        }

        // Animation Event
        private void Hit()
        {
            _combatTarget.TakeDamage(_weaponDamage);
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            return combatTarget.GetComponent<Health>().IsDead;
        }
    }
}