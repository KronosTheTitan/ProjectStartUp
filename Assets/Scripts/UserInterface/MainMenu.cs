using UnityEngine.UI;
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
        [SerializeField] private Image[] buttons;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unSelectedColor;
        
        [SerializeField] private int currentPosition = 0;
        public void Update()
        {
            if (Hinput.anyGamepad.dPad.up.justPressed || Hinput.anyGamepad.leftStick.up.justPressed)
            {
                currentPosition--;
                currentPosition = Mathf.Clamp(currentPosition, 0, options.Length - 1);
            }
            if (Hinput.anyGamepad.dPad.down.justPressed || Hinput.anyGamepad.leftStick.down.justPressed)
            {
                currentPosition++;
                currentPosition = Mathf.Clamp(currentPosition, 0, options.Length - 1);

            }

            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == currentPosition)
                {
                    buttons[i].color = selectedColor;
                }
                else
                {
                    buttons[i].color = unSelectedColor;
                }
            }
            
            if (Hinput.anyGamepad.A.justPressed)
            {
                switch (options[currentPosition])
                {
                    case MenuOptions.START:
                        StartNewGame();
                        break;
                    case MenuOptions.QUIT:
                        Application.Quit();
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
