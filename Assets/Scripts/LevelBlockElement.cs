using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelBlockElement : MonoBehaviour
{
    [SerializeField]
    string name;

    [SerializeField]
    int level;

    [SerializeField]
    TMP_Text text;


    private void Start()
    {
        SetLevelText();
    }

    void SetLevelText()
    {
        text.text = "Level " + level;
    }


}
