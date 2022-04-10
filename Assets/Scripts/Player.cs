using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    private bool isDead;
    private Vector3 movingToIfDead;

    public override void Fall()
    {
        GameManager.Instance.PlayerFalling();
        StartCoroutine(FadeOutMaterial());
        StartCoroutine(MoveFall());
    }
}

