using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBlock : Block
{
    [SerializeField] LayerMask[] layerMasks;
    [SerializeField] Material objectOnTopMat;
    [SerializeField] Material nothingOnTopMat;

    public bool IsCompleted;
    public bool NeedsToStandOnToTurnOn;
    bool hasBeenStoodOn;

    [SerializeField] GameObject[] gameObjects;

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

    void DoAction()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            StaticBlock staticBlock = gameObject.GetComponent<StaticBlock>();
            staticBlock.DoAction();
        }
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
                hasBeenStoodOn = true;
            }
        }

        if (NeedsToStandOnToTurnOn)
        {
            if (objectOnTop && !IsCompleted)
            {
                DoAction();
                GetComponent<Renderer>().material = objectOnTopMat;
                IsCompleted = true;
            }
            else if (IsCompleted)
            {
                DoAction();
                GetComponent<Renderer>().material = nothingOnTopMat;
                IsCompleted = false;
            }
        }
        else
        {
            if (hasBeenStoodOn && !IsCompleted)
            {
                DoAction();
                IsCompleted = true;
                GetComponent<Renderer>().material = objectOnTopMat;
            }
        }
    }
}
