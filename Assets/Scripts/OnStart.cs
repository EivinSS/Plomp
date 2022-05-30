using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    [SerializeField] List<GameObject> toDisable;
    void Start()
    {
        foreach (GameObject go in toDisable)
        {
            go.SetActive(false);
        }
    }
}