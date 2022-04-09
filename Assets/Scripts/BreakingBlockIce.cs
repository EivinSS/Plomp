using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlockIce : Block
{
    [SerializeField] int timesBeforeBreaking;

    public BreakingBlockIce()
    {
        blockType = BlockType.Ice;
        breakingBlock = true;
        timesBeforeBreaking = 1;
    }

    public override void DecrementTimesBeforeBreaking()
    {
        timesBeforeBreaking--;
    }

    public override bool CheckIfBreak()
    {
        if (timesBeforeBreaking == 0)
        {
            return true;
        }
        return false;
    }
}
