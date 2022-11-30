using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserInterface;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;
        private void Awake()
        {
            if(Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private LoadingScreen loadingScreen;
    
        /// <summary>
        /// Just starts the coroutine because that can only be done in the same class.
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsynchronously(sceneName));
        }

        /// <summary>
        /// This coroutine is used to load a scene in the background while displaying a loading screen.
        /// This way the game doesn't just freeze while loading a new scene.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        IEnumerator LoadSceneAsynchronously(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
            loadingScreen.StartLoadingScreen();
        
            while (!operation.isDone)
            {
                loadingScreen.UpdateLoadingScreen(operation.progress);
            
                yield return null;
            }
        
            loadingScreen.CloseLoadingScreen();
        }
    }
}
