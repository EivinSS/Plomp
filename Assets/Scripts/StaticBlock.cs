using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlock : Block
{
    [SerializeField] Vector3 toPosition;
    Vector3 originalPosition = default;
    bool toggle;

    private void Start()
    {
        originalPosition = transform.position;
        toggle = false;
    }
    public void DoAction()
    {
        Vector3 toPos = toggle ? originalPosition : toPosition;
        toggle = !toggle;
        GameManager.Instance.TellGMMoveStatus(gameObject.name, true, true);
        StartCoroutine(MoveToPos(toPos));
    }

    IEnumerator MoveToPos(Vector3 movingToTarget)
    {
        Vector3 diffVector = movingToTarget - transform.position;
        Vector3 stepDistance = Vector3.Scale(diffVector, new Vector3(0.05f, 0.05f, 0.05f));
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            Vector3 newPos = new Vector3(transform.position.x + stepDistance.x, transform.position.y + stepDistance.y, transform.position.z + stepDistance.z);
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }   
        GameManager.Instance.TellGMMoveStatus(gameObject.name, false, true);
        yield break;
    }
}
