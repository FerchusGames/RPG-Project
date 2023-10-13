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

        [field: SerializeField]
        public float Range { get; private set; } = 2f;

        [field: SerializeField]
        public float Damage { get; private set; } = 5f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (_equippedPrefab != null)
            {
                Instantiate(_equippedPrefab, handTransform);
            }

            if (_animatorOverrideController != null)
            {
                animator.runtimeAnimatorController = _animatorOverrideController;
            }
        }
    }
}
