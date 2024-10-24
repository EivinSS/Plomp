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
        FadeOutMaterials();
        StartCoroutine(MoveFall());
        GameManager.Instance.PlaySound(GameManager.SoundEnum.Death);
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

