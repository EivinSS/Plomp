using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevel : MonoBehaviour
{
    [SerializeField] GameEvent goToNextLevel;
    
    public void GoNext()
    {
        goToNextLevel.Raise();
    }
}
