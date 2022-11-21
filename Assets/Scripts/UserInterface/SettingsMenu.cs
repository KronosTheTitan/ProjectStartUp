using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserInterface;

public class SettingsMenu : Menu
{
    [SerializeField] private MainMenu mainMenu;
    public void BackToMainMenu()
    {
        mainMenu.OpenMenu();
    }
}
