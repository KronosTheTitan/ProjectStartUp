using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        
        /// <summary>
        /// Starts the loading screen.
        /// </summary>
        public void StartLoadingScreen()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes the loading screen.
        /// </summary>
        public void CloseLoadingScreen()
        {
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Updates the loading screen by the progress value
        /// of 0 to 1;
        /// </summary>
        /// <param name="progress"></param>
        public void UpdateLoadingScreen(float progress)
        {
            progressBar.value = progress;
        }
    }
}
