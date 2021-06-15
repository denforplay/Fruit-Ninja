using DG.Tweening;
using UnityEngine;

public class Rotate : IRotatable
{
    public void RotateObject(Block rotatingBlock)
    {
        rotatingBlock.transform.DORotate(new Vector3(0, 0, 360), 5.0f);
    }
}
