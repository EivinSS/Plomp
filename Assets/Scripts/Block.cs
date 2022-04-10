using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Block() {

    }
    public BlockType blockType = BlockType.Regular;
    public bool breakingBlock = false;


    public virtual void WasStoodOn(){}    

    public virtual void DecrementTimesBeforeBreaking(){}

    public virtual bool CheckIfBreak(){
        return false;
    }
}

public enum BlockType
{
    Regular,
    Ice,
    Fire,
    Button,
    Static
}
