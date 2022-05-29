using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public GameEvent GoToMainMenu;

    public void MainMenuButtonPressed() 
    {
        GoToMainMenu.Raise();
    }
}
