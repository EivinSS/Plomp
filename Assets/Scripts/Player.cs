using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{
    private bool isDead;
    private Vector3 movingToIfDead;
    public Player()
    {

    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Fall()
    {
        GameManager.Instance.PlayerFalling();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "WinningSphere")
        {
            Destroy(hit.gameObject);
            GameManager.Instance.LevelCleared();
        }
    }





}

