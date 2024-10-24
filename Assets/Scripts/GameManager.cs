using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool FreezeInput;
    public UnityAction MovementStopped;
    public delegate void WaitAndDoMethodDelegate();
    public WaitAndDoMethodDelegate waitAndDoMethodDelegate;
    private LevelObject levelObject;
    private WinningBlock winningBlock;
    private CanvasScript canvas;
    private SoundsManager soundsManager;
    private int totalCoinsOfLevel;
    private int currentCoins;
    private float fadeDuration = 1f;

    public GameEvent PlayerMovesNow;
    public GameEvent PlayerTriesAgainOrDies;
    public GameEvent PlayAd;
    public GameEvent PlayAdBanner;

    public Dictionary<string, bool> boolOfObjectsMoving;

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

    private void Start()
    {
        waitAndDoMethodDelegate = RestartLevel;
        boolOfObjectsMoving = new Dictionary<string, bool>();

        soundsManager = GameObject.Find("SoundsManager").GetComponent<SoundsManager>();

        levelObject = GameObject.Find("LevelObject").GetComponent<LevelObject>();
        totalCoinsOfLevel = levelObject.GetTotalCoinsOfLevel();

        winningBlock = GameObject.Find("Winning2").GetComponent<WinningBlock>();

        canvas = GameObject.Find("Canvas").GetComponent<CanvasScript>();
        canvas.SetToBlack();
        StartCoroutine(canvas.fadeToBright(fadeDuration));
    }

    public enum SoundEnum
    {
        PlayerMove,
        BlockMove,
        IceCrack,
        Winning,
        Coin,
        Death,
        ActivateGoal,
    }



    public void PlaySound(SoundEnum soundEnum)
    {
        soundsManager.PlaySound(soundEnum);
    }

    public void PlayerMoveSound(SoundEnum soundEnum)
    {
        soundsManager.PlayPlayerMoveSound(soundEnum);
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
        PlayerMovesNow.Raise();
        return true;
    }


    public void AddCoin()
    {
        currentCoins++;
        soundsManager.PlaySound(SoundEnum.Coin);

        if(CheckEnoughCoins())
        {
            EnoughCoins();
        }
    }

    public bool CheckEnoughCoins()
    {
        return currentCoins >= totalCoinsOfLevel;
    }

    public void EnoughCoins()
    {
        ActivateGoal();
        soundsManager.PlaySound(SoundEnum.ActivateGoal);
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
        Death();
    }
    public void Death()
    {
        int totalDeaths = PlayerPrefs.GetInt("Deaths", 0);
        int newDeaths = totalDeaths + 1;
        PlayerPrefs.SetInt("Deaths", newDeaths);
    }

        public void LevelCleared()
    {
        soundsManager.PlaySound(SoundEnum.Winning);
        StartCoroutine(DoLevelClearedPazaz());
    }

    IEnumerator DoLevelClearedPazaz()
    {
        yield return new WaitForSeconds(0.5f);
        FadeScreenToDarkness(0.5f);
        yield return new WaitForSeconds(0.5f);
        LevelClearedGoToNext();
    }

    void LoadCongratulations()
    {
        SceneManager.LoadScene("Congratulations");
    }

    public void LevelClearedGoToNext()
    {
        if (LastScene())
        {
            Debug.Log("You finished");
            LoadCongratulations();
            return;
        }

        Debug.Log("You cleared this level");
        if (PlayerPrefs.HasKey(NameConfig.currentMaxLevel))
        {
            //We are on max level
            if (levelObject.levelNumber == PlayerPrefs.GetInt(NameConfig.currentMaxLevel))
            {
                PlayerPrefs.SetInt(NameConfig.currentMaxLevel, levelObject.levelNumber + 1);
                Debug.Log("Increasing currentMaxLevel");
                int level = PlayerPrefs.GetInt(NameConfig.currentMaxLevel);
                string levelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == level).Key;
                SceneManager.LoadScene(levelName);
            }
            else
            {
                //Go to next level
                LoadNextLevel();
            }
        }
        else
        {
            Debug.Log("We dont have playerprefs");
            PlayerPrefs.SetInt(NameConfig.currentMaxLevel, levelObject.levelNumber + 1);
            string levelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == levelObject.levelNumber + 1).Key;
            SceneManager.LoadScene(levelName);
        }
        
    }

    public void LoadLevelWithBuildIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void LoadNextLevel()
    {
        string nextLevelName = NameConfig.levelDictionary.FirstOrDefault(levelDict => levelDict.Value == levelObject.levelNumber + 1).Key;
        SceneManager.LoadScene(nextLevelName);
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
