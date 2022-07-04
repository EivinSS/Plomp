using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] GameEvent Restart;

    public void OnRestartButtonClick()
    {
        if(Restart != null)
        {
            Restart.Raise();
        }
    }
}
