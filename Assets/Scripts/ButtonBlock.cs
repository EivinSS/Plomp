using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBlock : Block
{
    [SerializeField] LayerMask[] layerMasks;
    [SerializeField] Material objectOnTopMat;
    [SerializeField] Material nothingOnTopMat;

    private void Start()
    {
        GameManager.Instance.MovementStopped += CheckIfObjectOnTop;
    }

    private void OnDisable()
    {
        GameManager.Instance.MovementStopped -= CheckIfObjectOnTop;
    }

    public ButtonBlock()
    {
        blockType = BlockType.Button;
    }

    void CheckIfObjectOnTop()
    {
        bool objectOnTop = false;
        foreach (LayerMask layermask in layerMasks)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1.5f, layermask))
            {
                objectOnTop = true;
            }
        }
        if (objectOnTop)
        {
            GetComponent<Renderer>().material = objectOnTopMat;
        }
        else
        {
            GetComponent<Renderer>().material = nothingOnTopMat;
        }
    }
}
