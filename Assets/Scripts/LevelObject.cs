using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    public int numberOfCoins;
    public int levelNumber;

    public bool isLastLevel;

    public int GetTotalCoinsOfLevel()
    {
        return numberOfCoins;
    }

    public int GetLevel()
    {
        return levelNumber;
    }

    public bool IsLastLevel() {
        return isLastLevel;
    }
}
