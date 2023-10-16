using UnityEngine;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField]
        private float _timeBetweenAttacks = 1f;

        [SerializeField]
        private Transform _rightHandTransform = null;

        [SerializeField]
        private Transform _leftHandTransform = null;

        [SerializeField]
        private Weapon _defaultWeapon = null;

        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private Health _combatTarget;
        private Mover _mover;
        private Weapon _currentWeapon = null;

        private static int AP_ATTACK_TRIGGER = Animator.StringToHash("attack");
        private static int AP_STOP_ATTACK_TRIGGER = Animator.StringToHash("stopAttack");

        private float _timeSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            EquipWeapon(_defaultWeapon);
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_combatTarget == null)
                return;
            if (_combatTarget.IsDead)
                return;

            if (!IsInRange())
            {
                _mover.MoveTo(_combatTarget.transform.position, 1f);
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
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger(AP_STOP_ATTACK_TRIGGER);
            _animator.SetTrigger(AP_ATTACK_TRIGGER);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _combatTarget.transform.position)
                < _currentWeapon.Range;
        }

        public void Attack(GameObject combatTarget)
        {
            _actionScheduler.StartAction(this);
            _combatTarget = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            return !combatTarget.GetComponent<Health>().IsDead;
        }

        public void Cancel()
        {
            StopAttack();
            _combatTarget = null;
            _mover.Cancel();
        }

        private void StopAttack()
        {
            _animator.ResetTrigger(AP_ATTACK_TRIGGER);
            _animator.SetTrigger(AP_STOP_ATTACK_TRIGGER);
        }

        // Animation Event
        private void Hit()
        {
            if (_combatTarget == null)
                return;

            if (_currentWeapon.HasProjectile())
            {
                _currentWeapon.LaunchProjectile(
                    _rightHandTransform,
                    _leftHandTransform,
                    _combatTarget
                );
            }
            else
            {
                _combatTarget.TakeDamage(_currentWeapon.Damage);
            }
        }

        // Animation Event
        private void Shoot()
        {
            Hit();
        }

        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            _currentWeapon.Spawn(_rightHandTransform, _leftHandTransform, _animator);
        }
    }
}
