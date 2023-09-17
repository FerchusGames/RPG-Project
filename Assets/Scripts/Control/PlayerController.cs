using RPG.Movement;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
    
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out CombatTarget combatTarget))
                {
                    if (!_fighter.CanAttack(combatTarget)) continue;

                    if (Input.GetMouseButtonDown(0))
                    {
                        _fighter.Attack(combatTarget);
                    }
                    
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point);
                }
                
                return true;
            }

            return false;
        }
        
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}