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


    private void Start()
    {
        SetLevelText();
        CeckLevelAvailability();
        StartCoroutine(SetViewBasedOnAvailibility());
    }

    void SetLevelText()
    {
        text.text = "Level " + level;
    }

    void CeckLevelAvailability()
    {
        levelAvailable = level <= PlayerPrefs.GetInt(NameConfig.currentMaxLevel) ? true : false;
    }

    IEnumerator SetViewBasedOnAvailibility()
    {
        yield return new WaitForEndOfFrame();

        if (levelAvailable)
        {

            Color textColor = text.color;
            textColor.a = 1f;
            text.color = textColor;
        }
        else
        {
            Color textColor = text.color;
            textColor.a = 0.3f;
            text.color = textColor;
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
