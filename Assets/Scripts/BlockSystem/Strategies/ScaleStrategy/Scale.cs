using DG.Tweening;
using UnityEngine;

public class Scale : IScalable
{
    public void ScaleObject(Block rotatingBlock, float duration, float scale)
    {
        rotatingBlock.transform.DOScale(new Vector3(scale, scale, scale), duration);
    }
}
