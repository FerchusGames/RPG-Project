using UnityEngine.SceneManagement;
using UnityEngine;

namespace RPG
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene("Sandbox 2");
            }
        }
    }
}
