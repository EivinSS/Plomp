using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    private bool isDead;
    private Vector3 movingToIfDead;
    public GameEvent PlayerFalling;

    public override void Fall()
    {
        PlayerFalling.Raise();
        StartCoroutine(FadeOutMaterial());
        StartCoroutine(MoveFall());
    }

    public void Levitate()
    {
        StartCoroutine(DoLevitate());
    }

    IEnumerator DoLevitate()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
}

