using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private GameObject _weaponPrefab = null;

        [SerializeField]
        private AnimatorOverrideController _animatorOverrideController = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(_weaponPrefab, handTransform);
            animator.runtimeAnimatorController = _animatorOverrideController;
        }
    }
}
