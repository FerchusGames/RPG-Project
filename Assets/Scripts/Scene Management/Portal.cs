using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 1f;
        
        private Fader _fader;
        private SavingWrapper _savingWrapper;

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        
        private void Start()
        {
            _fader = FindObjectOfType<Fader>();
            _savingWrapper = FindObjectOfType<SavingWrapper>();
        }
        
        [SerializeField] private Transform _spawnPoint;
        
        [SerializeField] private int _sceneToLoad = -1;
        [SerializeField] private DestinationIdentifier _destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (_sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }
            
            DontDestroyOnLoad(gameObject);
            
            yield return StartCoroutine(_fader.FadeOut(_fadeDuration));

            _savingWrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(_sceneToLoad);
            
            _savingWrapper.Load();
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            _savingWrapper.Save();
            
            yield return StartCoroutine(_fader.FadeIn(_fadeDuration));

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            NavMeshAgent navMeshAgent = player.GetComponent<NavMeshAgent>();
            
            navMeshAgent.enabled = false;
            
            player.transform.position = otherPortal._spawnPoint.position;
            player.transform.rotation = otherPortal._spawnPoint.rotation;
            
            navMeshAgent.enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;

                if (portal._destination != _destination) continue;
                 
                return portal;
            }

            return null;
        }
    }
}
