using System;
using Managers;
using UnityEngine.UI;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    private enum MenuOptions
    {
        REPLAY,
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
                case MenuOptions.REPLAY:
                    SceneLoader.Instance.LoadScene("MainGame"); 
                    break;
                case MenuOptions.QUIT: 
                    SceneLoader.Instance.LoadScene("MainMenu"); 
                    break;
            }
        }
    }
}
