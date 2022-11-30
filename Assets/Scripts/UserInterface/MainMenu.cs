using System;
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
    public class MainMenu : MonoBehaviour, Menu
    {
        [SerializeField] private enum MenuOptions
        {
            START,
            QUIT
        }

        [SerializeField] private MenuOptions[] options;
        
        [SerializeField] private int currentPosition = 0;
        public void Update()
        {
            if (Hinput.anyGamepad.dPad.up.justPressed)
            {
                currentPosition--;
                currentPosition = Mathf.Clamp(currentPosition, 0, options.Length - 1);
            }
            if (Hinput.anyGamepad.dPad.down.justPressed)
            {
                currentPosition++;
                currentPosition = Mathf.Clamp(currentPosition, 0, options.Length - 1);

            }
            if (Hinput.anyGamepad.A.justPressed)
            {
                switch (options[currentPosition])
                {
                    case MenuOptions.START:
                        StartNewGame();
                        break;
                    case MenuOptions.QUIT:
                        break;
                }
            }
        }

        public void OpenMenu()
        {
            gameObject.SetActive(true);
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
    }
}
