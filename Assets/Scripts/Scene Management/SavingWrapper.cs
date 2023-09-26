using System;
using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string _defaultSaveFile = "save";
     
        [SerializeField] private float _fadeInTime = 0.5f;
        
        private SavingSystem _savingSystem;
        private Fader _fader;
        
        private void Awake()
        {
            _savingSystem = GetComponent<SavingSystem>();
        }
        
        private IEnumerator Start()
        {
            _fader = FindObjectOfType<Fader>();
            _fader.FadeOutImmediate();
            yield return _savingSystem.LoadLastScene(_defaultSaveFile);
            yield return _fader.FadeIn(_fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            _savingSystem.Save(_defaultSaveFile);
        }

        public void Load()
        {
            _savingSystem.Load(_defaultSaveFile);
        }
    }
}
