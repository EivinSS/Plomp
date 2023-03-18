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

    public override void WasStoodOn()
    {
        DecrementTimesBeforeBreaking();
        if (CheckIfBreak())
        {
            Destroy(gameObject);
        }
    }

    public override void DecrementTimesBeforeBreaking()
    {
        timesBeforeBreaking--;
        GameManager.Instance.PlaySound(GameManager.SoundEnum.IceCrack);
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
