using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float fadeDuration)
        {
            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime / fadeDuration;
                yield return null;
            }
        }
        
        public IEnumerator FadeIn(float fadeDuration)
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= Time.deltaTime / fadeDuration;
                yield return null;
            }
        }
    }
}
