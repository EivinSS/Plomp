using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePauseButton : MonoBehaviour
{
    [SerializeField] GameEvent disableSwipe;
    [SerializeField] GameEvent enableSwipe;

    public void DisableS()
    {
        disableSwipe.Raise();
    }

    public void EnableS()
    {
        enableSwipe.Raise();
    }
}
