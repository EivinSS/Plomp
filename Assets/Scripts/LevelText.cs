using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    private LevelObject levelObject;
    int level;
    [SerializeField] TMP_Text text;
    void Start()
    {
        levelObject = GameObject.Find("LevelObject").GetComponent<LevelObject>();
        if (levelObject != null)
        {
            level = levelObject.levelNumber;
            text.text += " " + level;
        }
    }
}
