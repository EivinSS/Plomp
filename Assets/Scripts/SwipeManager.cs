using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    [SerializeField] Player movingObject;
    private void Start()
    {
        movingObject = GetComponent<Player>();
        minDistanceSwipe = Screen.height / 10;
    }

    Vector2 fp;
    Vector2 lp;

    int minDistanceSwipe;

    private void Update()
    {
        Swipe();
    }

    void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lp = touch.position;

                if (Vector2.Distance(fp, lp) > minDistanceSwipe)
                {
                    if (Mathf.Abs(fp.x - lp.x) > Mathf.Abs(fp.y - lp.y))
                    {
                        //right
                        if (lp.x > fp.x)
                        {
                            IfCanMoveMove(MoveDirectionEnum.Right);
                        }
                        //left
                        else
                        {
                            IfCanMoveMove(MoveDirectionEnum.Left);
                        }
                    }
                    else
                    {
                        //up
                        if (lp.y > fp.y)
                        {
                            IfCanMoveMove(MoveDirectionEnum.Up);
                        }
                        //down
                        else
                        {
                            IfCanMoveMove(MoveDirectionEnum.Down);
                        }
                    }
                }
            }
        }
    }

    void IfCanMoveMove(MoveDirectionEnum moveDirection)
    {
        if (GameManager.Instance.CanAcceptInput())
        {
            movingObject.MoveInput(moveDirection);
        }
    }

}
