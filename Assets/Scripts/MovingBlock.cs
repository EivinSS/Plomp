using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MovingObject
{
    private bool canMakeSound;
    public MovingBlock()
    {
        moveSpeedAll = 15;
    }

    public override void Fall()
    {
        StartCoroutine(MoveFall());
        FadeOutMaterials();
        Destroy(gameObject, 2);
    }

    public override void Moving()
    {
        MakeSoundOnce();
    }

    public void PlayerMovesSetCanMakeSound()
    {
        canMakeSound = true;
    }

    public void MakeSoundOnce()
    {
        if(canMakeSound)
        {
            GameManager.Instance.PlaySound(GameManager.SoundEnum.BlockMove);
            canMakeSound = false;
        }
    }
}
