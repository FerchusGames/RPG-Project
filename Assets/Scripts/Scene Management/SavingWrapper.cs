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

        private void Save()
        {
            _savingSystem.Save(_defaultSaveFile);
        }

        private void Load()
        {
            _savingSystem.Load(_defaultSaveFile);
        }
    }
}
