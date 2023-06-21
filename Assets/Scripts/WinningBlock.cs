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

    void Start()
    {
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        yield return new WaitForEndOfFrame();

        if (GameManager.Instance.CheckEnoughCoins())
        {
            SetWinningSphereInitially();
        }
        else
        {
            winningSphere.SetActive(false);
            //magicRing.SetActive(false);
        }
    }

    void SetWinningSphereInitially()
    {
        //ActivateMagicRing();
        winningSphere.SetActive(true);
    }
    public void ActivateWinningSphere()
    {
        //SetWinningMat();
        //ActivateMagicRing();
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
