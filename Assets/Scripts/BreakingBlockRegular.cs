using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBlockRegular : Block
{
    [SerializeField] int timesBeforeBreaking;

    public BreakingBlockRegular()
    {
        blockType = BlockType.Regular;
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
