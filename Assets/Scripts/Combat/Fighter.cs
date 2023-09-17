using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        
        private ActionScheduler _actionScheduler;

        private Animator _animator;
        private Transform _combatTarget;
        private Mover _mover;
        
        private static int AP_ATTACK_TRIGGER = Animator.StringToHash("attack");
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
            
            if (!IsInRange())
            {
                _mover.MoveTo(_combatTarget.position);
            }
            
            else
            {
                _mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (_timeSinceLastAttack > _timeBetweenAttacks)
            {
                _animator.SetTrigger(AP_ATTACK_TRIGGER);
                _timeSinceLastAttack = 0;
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _combatTarget.position) < _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _combatTarget = combatTarget.transform;
        }

        public void Cancel()
        {
            _combatTarget = null;
        }

        // Animation Event
        private void Hit()
        {
            
        }
    }
}