using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MovingObject
{
    public MovingBlock()
    {
        moveSpeedAll = 15;
    }

    public override void Fall()
    {
        StartCoroutine(MoveFall());
        StartCoroutine(FadeOutMaterial());
        Destroy(gameObject, 2);
    }
}
