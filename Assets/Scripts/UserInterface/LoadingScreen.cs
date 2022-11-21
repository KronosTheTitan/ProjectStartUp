using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class LoadingScreen : MonoBehaviour
    {
        public void StartLoadingScreen()
        {
            gameObject.SetActive(true);
        }

        public void CloseLoadingScreen()
        {
            gameObject.SetActive(false);
        }

        [SerializeField] private Slider progressBar;
        public void UpdateLoadingScreen(float progress)
        {
            progressBar.value = progress;
        }
    }
}
