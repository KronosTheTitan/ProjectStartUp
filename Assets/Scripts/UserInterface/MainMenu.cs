using System.Collections;
using Managers;
using UnityEngine;

namespace UserInterface
{
    /// <summary>
    /// This class is used to control the main menu.
    /// It contains functions for handling all the buttons
    /// on the main menu.
    /// </summary>
    public class MainMenu : Menu
    {
        public override void OpenMenu()
        {
            gameObject.SetActive(true);
            //_animator.SetTrigger("");
        }

        [SerializeField] private string game;

        /// <summary>
        /// Actually starts the new game.
        /// It is called from an animation event.
        /// </summary>
        public void StartNewGame()
        {
            gameObject.SetActive(false);
            SceneLoader.Instance.LoadScene(game);
        }
    
        /// <summary>
        /// This method closes the application.
        /// </summary>
        public void QuitToDesktop()
        {
            Application.Quit();
        }

        public void StartAsHost()
        {
            
        }

        public void JoinAsClient()
        {
            
        }
    }
}
