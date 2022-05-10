using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool FreezeInput;
    public UnityAction MovementStopped;
    public delegate void WaitAndDoMethodDelegate();
    public WaitAndDoMethodDelegate waitAndDoMethodDelegate;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private LevelObject levelObject;
    private WinningBlock winningBlock;
    private CanvasScript canvas;
    private int totalCoinsOfLevel;
    private int currentCoins;
    private float fadeDuration = 1f;

    private Dictionary<string, bool> boolOfObjectsMoving;

    private void Start()
    {
        waitAndDoMethodDelegate = RestartLevel;
        boolOfObjectsMoving = new Dictionary<string, bool>();

        levelObject = GameObject.Find("LevelObject").GetComponent<LevelObject>();
        totalCoinsOfLevel = levelObject.GetTotalCoinsOfLevel();

        winningBlock = GameObject.Find("WinningBlock").GetComponent<WinningBlock>();

        canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvas.SetToBlack();
        StartCoroutine(canvas.fadeToBright(fadeDuration));
    }

    public void TellGMMoveStatus(string name, bool isMoving, bool doNotInvokeMoveStopped = false)
    {
        bool exist = boolOfObjectsMoving.ContainsKey(name);
        if (exist)
        {
            boolOfObjectsMoving[name] = isMoving;
        }
        else
        {
            boolOfObjectsMoving.Add(name, isMoving);
        }

        if (!isMoving && !doNotInvokeMoveStopped)
        {
            MovementStopped?.Invoke();
        }
    }

    public bool CanAcceptInput()
    {
        foreach (var kvp in boolOfObjectsMoving)
        {
            if (kvp.Value)
            {
                return false;
            }
        }
        boolOfObjectsMoving.Clear();
        return true;
    }


    public void AddCoin()
    {
        currentCoins++;
        CheckEnoughCoins();
    }

    private void CheckEnoughCoins()
    {
        if (currentCoins >= totalCoinsOfLevel)
        {
            ActivateGoal();
        }
    }

    private void ActivateGoal()
    {
        winningBlock.ActivateWinningSphere();
    }

    public void FadeScreenToDarkness(float fadeDuration)
    {
        StartCoroutine(canvas.fadeToDarkness(fadeDuration));
    }

    public void WaitDoMethodDelegateEvent(float waitTime)
    {
        StartCoroutine(WaitForSecondsAndDoMethod(waitTime, waitAndDoMethodDelegate));
    }

    IEnumerator WaitForSecondsAndDoMethod(float seconds, WaitAndDoMethodDelegate method)
    {
        yield return new WaitForSeconds(seconds);
        method();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelCleared()
    {
        if (LastScene())
        {
            Debug.Log("You finished");
            return;
        }
        Debug.Log("You cleared this level");
        if (PlayerPrefs.HasKey(NameConfig.currentMaxLevel))
        {
            if (levelObject.levelNumber == PlayerPrefs.GetInt(NameConfig.currentMaxLevel))
            {
                PlayerPrefs.SetInt(NameConfig.currentMaxLevel, levelObject.levelNumber + 1);
                Debug.Log("Increasing currentMaxLevel");
            }
        }
        else
        {
            Debug.Log("We dont have playerprefs");
        }
        int level = PlayerPrefs.GetInt(NameConfig.currentMaxLevel);
        string levelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == level).Key;
        SceneManager.LoadScene(levelName);
    }

    private bool LastScene()
    {
        if (levelObject.IsLastLevel())
        {
            return true;
        }
        return false;
    }
}
