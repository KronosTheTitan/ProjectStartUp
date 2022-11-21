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
    
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneAsynchronously(sceneName));
        }

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
