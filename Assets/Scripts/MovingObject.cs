using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int moveDistance = 2;
    public Vector3 MovingDirection;
    public int moveSpeedAll = 5;
    public int fallSpeed = 25;
    public bool moving;
    public bool isFalling;
    public Vector3 StartPositionPlayer;
    [SerializeField] LayerMask blockLayerMask;
    [SerializeField] LayerMask movingBlockLayerMask;
    [SerializeField] LayerMask staticBlockLayerMask;
    [SerializeField] LayerMask coinLayerMask;
    private CharacterController characterController;
    private Vector3 movingToTarget;
    private Vector3 fallingToTarget;

    protected virtual void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        enabled = false;
    }
    void Update()
    {
        if (isFalling)
        {
            SimpleFall();
        }
        if (!moving)
        {
            return;
        }
        else
        {
            MoveTowardsTarget(movingToTarget);
        }
    }

    void SimpleFall()
    {
        characterController.Move(Vector3.down * Time.deltaTime * fallSpeed);
    }

    void MoveTowardsTarget(Vector3 target)
    {
        Vector3 offset = target - transform.position;
        if (offset.magnitude > .1f)
        {
            offset = offset.normalized;
            characterController.Move(offset * Time.deltaTime * moveSpeedAll);
        }
        else
        {
            RoundPlayerPosition();
            MoveDirectionEnum directionEnum = GetMovementDirectionEnum(MovingDirection);
            Block block = TryFindBlockObjectStandingOn();
            if (block != null)
            {
                if (staticBlockInFront(directionEnum))
                {
                    moving = false;
                    GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
                    enabled = false;
                    return;
                }
                if (block.blockType == BlockType.Ice)
                {
                    Coin coin = TryGetCoin(directionEnum);
                    if (coin != null)
                    {
                        if (this.gameObject.tag == "Player")
                        {
                            coin.CollectCoin();
                        }
                        else if (this.gameObject.tag == "MovingBlock")
                        {
                            moving = false;
                            GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
                            enabled = false;
                            return;
                        }
                    }
                    MovingObject movingObjectFront = TryFindMovingObjectInFront(directionEnum);
                    if (movingObjectFront != null)
                    {
                        if (chainCheck(directionEnum) >= 2)
                        {
                            moving = false;
                            GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
                            enabled = false;
                            return;
                        }
                        movingObjectFront.MoveInput(directionEnum);
                        moving = false;
                        GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
                        enabled = false;
                        return;
                    }
                    else
                    {
                        //BreakingBlockIce
                        if (block.breakingBlock)
                        {
                            block.DecrementTimesBeforeBreaking();
                            if (block.CheckIfBreak())
                            {
                                Destroy(block.gameObject);
                            }
                        }
                        //Sliding, update target position
                        movingToTarget = MovingDirection * moveDistance + transform.position;
                        return;
                    }
                }
            }
            moving = false;
            GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
            DeathCheck();
        }
    }

    public void MoveInput(MoveDirectionEnum moveDirection)
    {
        Vector3 direction = GetMovementDirection(moveDirection);
        if (staticBlockInFront(moveDirection))
        {
            return;
        }
        if (chainCheck(moveDirection) >= 2)
        {
            return;
        }
        MovingObject movingObject = TryFindMovingObjectInFront(moveDirection);
        if (movingObject != null)
        {
            movingObject.MoveInput(moveDirection);
            return;
        }
        Block blockBelow = TryFindBlockObjectStandingOn();
        if (blockBelow != null)
        {
            if (blockBelow.breakingBlock)
            {
                blockBelow.DecrementTimesBeforeBreaking();
                if (blockBelow.CheckIfBreak())
                {
                    Destroy(blockBelow.gameObject);
                }
            }

            Coin coin = TryGetCoin(moveDirection);
            if (coin != null)
            {
                if (this.gameObject.tag == "Player")
                {
                    coin.CollectCoin();
                }
                else if (this.gameObject.tag == "MovingBlock")
                {
                    return;
                }
            }

            

            movingToTarget = (direction * moveDistance) + transform.position;
            MovingDirection = direction;
            moving = true;
            GameManager.Instance.TellGMMoveStatus(gameObject.name, true);
            enabled = true;
        }
        else
        {
            DeathCheck();
        }
    }

    Coin TryGetCoin(MoveDirectionEnum moveDirectionEnum)
    {
        {
            RaycastHit hit;
            Vector3 moveDirectionNormalize = GetMovementDirection(moveDirectionEnum);
            if (Physics.Raycast(transform.position, transform.TransformDirection(moveDirectionNormalize), out hit, 2.5f, coinLayerMask))
            {
                GameObject coinObject = hit.collider.gameObject;
                if (coinObject.TryGetComponent(out Coin coinScript))
                {
                    return coinScript;
                }
            }
            return null;
        }

    }

    int chainCheck(MoveDirectionEnum moveDirectionEnum)
    {
        int chain = 0;
        Vector3 moveDirectionNormalize = GetMovementDirection(moveDirectionEnum);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(moveDirectionNormalize), 4f, movingBlockLayerMask);
        List<GameObject> seenGameObjects = new List<GameObject>();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != this.gameObject && !seenGameObjects.Contains(hit.collider.gameObject))
            {
                GameObject go = hit.collider.gameObject;
                MovingObject movingObject = go.GetComponent<MovingObject>();
                if (movingObject != null)
                {
                    Block block = movingObject.TryFindBlockObjectStandingOn();
                    if (block != null)
                    {
                        if (block.blockType == BlockType.Ice)
                        {
                            seenGameObjects.Add(hit.collider.gameObject);
                            continue;
                        }
                    }

                }
                seenGameObjects.Add(hit.collider.gameObject);
                chain++;
            }
        }
        return chain;
    }

    bool staticBlockInFront(MoveDirectionEnum moveDirectionEnum)
    {
        Vector3 moveDirectionNormalize = GetMovementDirection(moveDirectionEnum);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(moveDirectionNormalize), 1.5f, staticBlockLayerMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    MovingObject TryFindMovingObjectInFront(MoveDirectionEnum moveDirectionEnum)
    {
        Vector3 moveDirectionNormalize = GetMovementDirection(moveDirectionEnum);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(moveDirectionNormalize), 1.5f, movingBlockLayerMask);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                GameObject BlockObject = hit.collider.gameObject;
                if (BlockObject.TryGetComponent(out MovingObject movingObject))
                {
                    return movingObject;
                }
            }
        }
        return null;
    }

    public Block TryFindBlockObjectStandingOn()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f, blockLayerMask))
        {
            GameObject BlockObject = hit.collider.gameObject;
            if (BlockObject.TryGetComponent(out Block blockScript))
            {
                return blockScript;
            }
        }
        return null;
    }
    void DeathCheck()
    {
        Block block = TryFindBlockObjectStandingOn();
        if (block == null)
        {
            isFalling = true;
            Fall();
        }
    }

    public virtual void Fall()
    {
        Debug.Log("NOO");
    }

    Vector3 GetMovementDirection(MoveDirectionEnum moveDirection)
    {
        if (moveDirection == MoveDirectionEnum.Up)
        {
            return new Vector3(0, 0, 1);
        }
        if (moveDirection == MoveDirectionEnum.Down)
        {
            return new Vector3(0, 0, -1);
        }
        if (moveDirection == MoveDirectionEnum.Right)
        {
            return new Vector3(1, 0, 0);
        }
        if (moveDirection == MoveDirectionEnum.Left)
        {
            return new Vector3(-1, 0, 0);
        }
        return new Vector3(0, 0, 0);
    }
    MoveDirectionEnum GetMovementDirectionEnum(Vector3 direction)
    {
        if (direction == new Vector3(0, 0, 1))
        {
            return MoveDirectionEnum.Up;
        }
        if (direction == new Vector3(0, 0, -1))
        {
            return MoveDirectionEnum.Down;
        }
        if (direction == new Vector3(1, 0, 0))
        {
            return MoveDirectionEnum.Right;
        }
        if (direction == new Vector3(-1, 0, 0))
        {
            return MoveDirectionEnum.Left;
        }
        return MoveDirectionEnum.Up;
    }
    void RoundPlayerPosition()
    {
        float x = Mathf.Round(transform.position.x);
        float y = transform.position.y;
        float z = Mathf.Round(transform.position.z);
        transform.position = new Vector3(x, y, z);
    }
}
public enum MoveDirectionEnum
{
    Up,
    Down,
    Right,
    Left
}
