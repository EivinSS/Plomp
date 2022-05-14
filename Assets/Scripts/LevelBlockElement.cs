using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBlockElement : MonoBehaviour
{
    bool levelAvailable;

    [SerializeField]
    string levelName;

    [SerializeField]
    int level;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    Image buttonImage;


    private void Start()
    {
        SetLevelText();
        CeckLevelAvailability();
        SetViewBasedOnAvailibility();
    }

    void SetLevelText()
    {
        text.text = "Level " + level;
    }

    void CeckLevelAvailability()
    {
        levelAvailable = level <= PlayerPrefs.GetInt(NameConfig.currentMaxLevel) ? true : false;
    }

    void SetViewBasedOnAvailibility()
    {
        if (levelAvailable)
        {
            Color c = buttonImage.color;
            c.a = 0f;
            buttonImage.color = c;
        }
        else
        {
            Color c = buttonImage.color;
            c.a = 0.2f;
            buttonImage.color = c;
        }

        
    }

    public void LevelElementButtonClick()
    {
        if (levelAvailable)
        {
            string levelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == level).Key;
            SceneManager.LoadScene(levelName);
        }
    }
}
