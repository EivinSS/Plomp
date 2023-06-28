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
            StartCoroutine(MoveFall());
            FadeOutMaterials();
            Destroy(gameObject, 2); ;
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

    public IEnumerator MoveFall()
    {
        Vector3 movingToTarget;
        float moveSpeedAll = 10;
        movingToTarget = new Vector3(transform.position.x, transform.position.y - 20f, transform.position.z);
        while (Vector3.Distance(transform.position, movingToTarget) >= 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, movingToTarget, Time.deltaTime * moveSpeedAll);
            yield return null;
        }
        yield break;
    }

    public virtual void FadeOutMaterials()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        foreach (Material m in renderer.materials)
        {
            StartCoroutine(FadeOutMaterial(m));
        }
    }

    IEnumerator FadeOutMaterial(Material m)
    {
        float elapsedTime = 0f;
        Color initialColor = m.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (elapsedTime < 2f)
        {
            elapsedTime += 0.01f;
            m.color = Color.Lerp(initialColor, targetColor, elapsedTime / 2f);
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }
}
