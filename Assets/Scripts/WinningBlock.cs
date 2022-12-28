using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningBlock : MonoBehaviour
{
    [SerializeField] Material winningMat;
    [SerializeField] GameObject winningSphere;
    [SerializeField] GameObject magicRing;
    [SerializeField] Vector3 toPosition;

    private void Start()
    {
        winningSphere.SetActive(false);
    }
    public void ActivateWinningSphere()
    {
        //SetWinningMat();
        ActivateMagicRing();
        winningSphere.SetActive(true);
        MoveBlock();
    }

    private void ActivateMagicRing()
    {
        magicRing.SetActive(true);
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
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingToTarget, Time.deltaTime * 2f);
            yield return null;
        }
        GameManager.Instance.TellGMMoveStatus(gameObject.name, false, true);
        yield break;
    }
}
