using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MovingObject
{
    public MovingBlock() {
        moveSpeedAll = 15;
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Fall()
    {
        Destroy(gameObject, 2);
    }
}
