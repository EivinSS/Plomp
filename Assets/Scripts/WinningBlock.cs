using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningBlock : MonoBehaviour
{
    [SerializeField] Material winningMat;
    [SerializeField] GameObject winningSphere;
    [SerializeField] Vector3 toPosition;

    private void Start()
    {
        winningSphere.SetActive(false);
    }
    public void ActivateWinningSphere()
    {
        SetWinningMat();
        winningSphere.SetActive(true);
        MoveBlock();
    }

    void SetWinningMat()
    {
        GetComponent<Renderer>().material = winningMat;
    }

    void MoveBlock()
    {
        Vector3 toPos = toPosition;
        GameManager.Instance.TellGMMoveStatus(gameObject.name, true, true);
        StartCoroutine(MoveToPos(toPos));
    }

    IEnumerator MoveToPos(Vector3 movingToTarget)
    {
        Vector3 diffVector = movingToTarget - transform.position;
        Vector3 stepDistance = Vector3.Scale(diffVector, new Vector3(0.025f, 0.025f, 0.025f));
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            Vector3 newPos = new Vector3(transform.position.x + stepDistance.x, transform.position.y + stepDistance.y, transform.position.z + stepDistance.z);
            transform.position = newPos;
            yield return new WaitForSeconds(0.02f);
        }
        GameManager.Instance.TellGMMoveStatus(gameObject.name, false, true);
        yield break;
    }
}
