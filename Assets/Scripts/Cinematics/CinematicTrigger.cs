using RPG.Saving;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
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

        public object CaptureState()
        {
            return _alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            _alreadyTriggered = (bool)state;
        }
    }
}
