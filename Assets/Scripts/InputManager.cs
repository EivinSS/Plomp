using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player movingObject;
    private void Start()
    {
        movingObject = GetComponent<Player>();
    }
    public void ButtonPressUp()
    {
        if (GameManager.Instance.CanAcceptInput())
        {
            movingObject.MoveInput(MoveDirectionEnum.Up);
        }
    }
    public void ButtonPressDown()
    {
        if (GameManager.Instance.CanAcceptInput())
        {
            movingObject.MoveInput(MoveDirectionEnum.Down);
        }
    }
    public void ButtonPressRight()
    {
        if (GameManager.Instance.CanAcceptInput())
        {
            movingObject.MoveInput(MoveDirectionEnum.Right);
        }
    }
    public void ButtonPressLeft()
    {
        if (GameManager.Instance.CanAcceptInput())
        {
            movingObject.MoveInput(MoveDirectionEnum.Left);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movingObject.MoveInput(MoveDirectionEnum.Up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            movingObject.MoveInput(MoveDirectionEnum.Down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movingObject.MoveInput(MoveDirectionEnum.Right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            movingObject.MoveInput(MoveDirectionEnum.Left);
        }
    }
}
