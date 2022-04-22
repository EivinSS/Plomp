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
}

