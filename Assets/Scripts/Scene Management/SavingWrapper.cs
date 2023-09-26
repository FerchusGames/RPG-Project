using System;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string _defaultSaveFile = "save";
        
        private SavingSystem _savingSystem;
        
        private void Awake()
        {
            _savingSystem = GetComponent<SavingSystem>();
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
