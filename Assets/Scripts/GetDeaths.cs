using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetDeaths : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    void Start()
    {
        //Get deaths
        int deaths = PlayerPrefs.GetInt("Deaths", 0);
        text.text = deaths + "";
    }
}
