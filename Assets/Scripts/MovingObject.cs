using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public int moveDistance = 2;
    public Vector3 MovingDirection;
    public int moveSpeedAll = 5;
    public int fallSpeed = 25;
    public Vector3 StartPositionPlayer;
    [SerializeField] LayerMask blockLayerMask;
    [SerializeField] LayerMask movingBlockLayerMask;
    [SerializeField] LayerMask staticBlockLayerMask;
    [SerializeField] LayerMask coinLayerMask;
    [SerializeField] LayerMask winningSphereMask;
    private Vector3 movingToTarget;
    private Vector3 fallingToTarget;

    public void MoveInput(MoveDirectionEnum moveDirection)
    {
        Vector3 direction = GetMovementDirection(moveDirection);

        if (staticBlockInFront(direction))
        {
            return;
        }
        if (chainCheck(direction) > 2)
        {
            return;
        }
        MovingObject movingObject = TryFindMovingObjectInFront(direction);
        if (movingObject != null)
        {
            movingObject.MoveInput(moveDirection);
            return;
        }

        Coin coin = TryGetCoin(direction);
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


        if (CheckIfWinningSphere(direction))
        {
            if (this.gameObject.tag == "Player")
            {
                GameManager.Instance.LevelCleared();
            }
            else if (this.gameObject.tag == "MovingBlock")
            {
                return;
            }
        }

        Block blockBelow = TryFindBlockObjectStandingOn();
        if (blockBelow != null)
        {
            if (blockBelow.breakingBlock)
            {
                blockBelow.WasStoodOn();
            }

            movingToTarget = (direction * moveDistance) + transform.position;
            MovingDirection = direction;
            GameManager.Instance.TellGMMoveStatus(gameObject.name, true);
            StartCoroutine(MoveStep(direction));
        }
    }

    IEnumerator MoveStep(Vector3 direction)
    {
        float dist = (float)moveDistance;
        movingToTarget = (direction * moveDistance) + transform.position;
        Vector3 stepDistance = Vector3.Scale(Vector3.Scale(direction, new Vector3(dist, dist, dist)), new Vector3(0.04f, 1f, 0.04f));
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            Vector3 newPos = new Vector3(transform.position.x + stepDistance.x, transform.position.y, transform.position.z + stepDistance.z);
            transform.position = newPos;
            yield return new WaitForSeconds(0.008f);
        }
        RoundPlayerPosition();
        GameManager.Instance.TellGMMoveStatus(gameObject.name, false);
        AfterMoveInput(direction);
        yield break;
    }

    public IEnumerator MoveFall()
    {
        Vector3 direction = new Vector3(0, -1f, 0);
        movingToTarget = new Vector3(transform.position.x, transform.position.y - 20f, transform.position.z);
        Vector3 stepDistance = new Vector3(0, -0.1f, 0);
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + stepDistance.y, transform.position.z);
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }
        yield break;
    }

    void AfterMoveInput(Vector3 direction)
    {
        if (DeathCheck())
        {
            return;
        }
        Block block = TryFindBlockObjectStandingOn();
        if (block.blockType == BlockType.Ice)
        {
            MoveInput(GetMovementDirectionEnum(direction));
        }
    }

    Coin TryGetCoin(Vector3 moveDirectionNormalize)
    {
        {
            RaycastHit hit;
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

    bool CheckIfWinningSphere(Vector3 moveDirectionNormalize)
    {
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(moveDirectionNormalize), out hit, 2.5f, winningSphereMask))
            {
                return true;
            }
            return false;
        }
    }

    int chainCheck(Vector3 moveDirectionNormalize)
    {
        int chain = 0;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(moveDirectionNormalize), 6f, movingBlockLayerMask);
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
                        chain++;
                    }
                }
                seenGameObjects.Add(hit.collider.gameObject);
            }
        }
        return chain;
    }

    bool staticBlockInFront(Vector3 moveDirectionNormalize)
    {
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

    MovingObject TryFindMovingObjectInFront(Vector3 moveDirectionNormalize)
    {
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
    bool DeathCheck()
    {
        Block block = TryFindBlockObjectStandingOn();
        if (block == null)
        {
            Fall();
            return true;
        }
        return false;
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

    public virtual IEnumerator FadeOutMaterial()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        float elapsedTime = 0f;
        Color initialColor = renderer.material.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (elapsedTime < 2f)
        {
            elapsedTime += 0.01f;
            renderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / 2f);
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }
}
public enum MoveDirectionEnum
{
    Up,
    Down,
    Right,
    Left
}
