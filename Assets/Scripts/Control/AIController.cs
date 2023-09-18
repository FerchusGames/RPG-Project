using System;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float _chaseDistance = 5f;

        private Fighter _fighter;
        private Health _health;
        
        private GameObject _player;
        
        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead) return;

            if (ShouldChasePlayer() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }

        private bool ShouldChasePlayer()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            return distanceToPlayer < _chaseDistance;
        }
    }
}
