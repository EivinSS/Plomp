using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfEditor : MonoBehaviour
{
#if UNITY_EDITOR
    void Start()
    {
        this.gameObject.SetActive(true);
    }
#else
    void Start() 
    {
        this.gameObject.SetActive(false);
    }
#endif
}
