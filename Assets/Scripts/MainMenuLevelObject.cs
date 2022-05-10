using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Linq;
public class MainMenuLevelObject : MonoBehaviour
{
    private CanvasScript canvas;
    private float fadeDuration = 1f;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvas.SetToBlack();
        StartCoroutine(canvas.fadeToBright(fadeDuration));

        if (!PlayerPrefs.HasKey(NameConfig.currentMaxLevel))
        {
            PlayerPrefs.SetInt(NameConfig.currentMaxLevel, 1);
            Debug.Log("setting level playerprefs first time");
        }
    }

    public void pressPlay()
    {
        if (PlayerPrefs.HasKey(NameConfig.currentMaxLevel))
        {
            int level = PlayerPrefs.GetInt(NameConfig.currentMaxLevel);
            string levelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == level).Key;
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.Log("Ops, we dont have your level");
        }
    }

    public void pressLevels()
    {
        SceneManager.LoadScene(NameConfig.levels);
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene(NameConfig.mainMenu);
    }

    public void pressReset()
    {
        PlayerPrefs.SetInt(NameConfig.currentMaxLevel, 1);
        Debug.Log("Setting level to 1 again");
    }
}
