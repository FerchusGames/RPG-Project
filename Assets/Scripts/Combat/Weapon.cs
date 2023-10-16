using UnityEngine;

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

        [field: SerializeField]
        public float Range { get; private set; } = 2f;

        [field: SerializeField]
        public float Damage { get; private set; } = 5f;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (_equippedPrefab != null)
            {
                Transform handTransform = _isRightHanded ? rightHand : leftHand;
                Instantiate(_equippedPrefab, handTransform);
            }

            if (_animatorOverrideController != null)
            {
                animator.runtimeAnimatorController = _animatorOverrideController;
            }
        }
    }
}
