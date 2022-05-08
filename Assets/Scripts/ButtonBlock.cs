using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBlock : Block
{
    [SerializeField] LayerMask[] layerMasks;
    [SerializeField] Material objectOnTopMat;
    [SerializeField] Material nothingOnTopMat;

    public bool NeedsToStandOnToTurnOn;
    bool isCompleted;
    bool hasBeenStoodOn;
    bool prevBeenStoodOn;

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

        //Needs to stand on blocks
        if (NeedsToStandOnToTurnOn)
        {
            if (!prevBeenStoodOn)
            {
                if (objectOnTop)
                {
                    DoAction();
                    GetComponent<Renderer>().material = objectOnTopMat;
                    prevBeenStoodOn = true;
                }
            }
            else if(!objectOnTop)
            {
                DoAction();
                GetComponent<Renderer>().material = nothingOnTopMat;
                prevBeenStoodOn = false;
            }
        }
        //Stand on once blocks
        else
        {
            if (hasBeenStoodOn && !isCompleted)
            {
                DoAction();
                isCompleted = true;
                GetComponent<Renderer>().material = objectOnTopMat;
            }
        }
    }
}
