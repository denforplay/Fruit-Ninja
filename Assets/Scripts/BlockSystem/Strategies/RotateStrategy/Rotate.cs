using DG.Tweening;
using UnityEngine;

public class Rotate : IRotatable
{
    public void RotateObject(Block rotatingBlock, float duration)
    {
        rotatingBlock.transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360);
    }
}
