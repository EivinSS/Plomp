using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public GameEvent GoToMainMenu;

    public void MainMenuButtonPressed() 
    {
        int totalDeaths = PlayerPrefs.GetInt("Deaths", 0);
        int newDeaths = totalDeaths + 1;
        PlayerPrefs.SetInt("Deaths", newDeaths);
        GoToMainMenu.Raise();
    }
}
