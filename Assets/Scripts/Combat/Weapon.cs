using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private GameObject _equippedPrefab = null;

        [SerializeField]
        private AnimatorOverrideController _animatorOverrideController = null;

        [SerializeField]
        private bool _isRightHanded = true;

        [SerializeField]
        private Projectile _projectile = null;

        [field: SerializeField]
        public float Range { get; private set; } = 2f;

        [field: SerializeField]
        public float Damage { get; private set; } = 5f;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (_equippedPrefab != null)
            {
                Instantiate(_equippedPrefab, GetHandTransform(rightHand, leftHand));
            }

            if (_animatorOverrideController != null)
            {
                animator.runtimeAnimatorController = _animatorOverrideController;
            }
        }

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return _isRightHanded ? rightHand : leftHand;
        }

        public bool HasProjectile()
        {
            return _projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(
                _projectile,
                GetHandTransform(rightHand, leftHand).position,
                Quaternion.identity
            );
            projectileInstance.SetTarget(target, Damage);
        }
    }
}
