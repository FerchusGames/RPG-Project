using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private PlayableDirector _playableDirector;

        private bool _alreadyTriggered = false;
        
        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_alreadyTriggered && other.gameObject.CompareTag("Player"))
            {
                _alreadyTriggered = true;
                _playableDirector.Play();
            }
        }
    }
}
