using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuLevelObject : MonoBehaviour
{
    private CanvasScript canvas;
    private float fadeDuration = 1f;

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvas.SetToBlack();
        StartCoroutine(canvas.fadeToBright(fadeDuration));

        if (!PlayerPrefs.HasKey(NameConfig.levelCleared))
        {
            PlayerPrefs.SetInt(NameConfig.levelCleared, 0);
            Debug.Log("setting level playerprefs first time");
        }
    }

    public void pressPlay()
    {
        if (PlayerPrefs.HasKey(NameConfig.levelCleared))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(NameConfig.levelCleared));
        }
        else
        {
            Debug.Log("Ops, we dont have your level");
        }
    }
}
