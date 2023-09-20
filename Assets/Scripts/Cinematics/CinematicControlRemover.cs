using RPG.Core;
using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _playableDirector;
        private GameObject _player;
        private PlayerController _playerController;
        private ActionScheduler _actionScheduler;

        private void Awake()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            _player = GameObject.FindWithTag("Player");
            _actionScheduler = _player.GetComponent<ActionScheduler>();
            _playerController = _player.GetComponent<PlayerController>();
        }

        private void Start()
        {
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }

        private void DisableControl(PlayableDirector playableDirector)
        {
            _actionScheduler.CancelCurrentAction();
            _playerController.enabled = false;
        }
        
        private void EnableControl(PlayableDirector playableDirector)
        {
            _playerController.enabled = true;
        }
    }
}
