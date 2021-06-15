using DG.Tweening;
using UnityEngine;

public class Scale : IScalable
{
    public void ScaleObject(Block rotatingBlock)
    {
        rotatingBlock.transform.DORotate(new Vector3(0.5f, 0.5f), 5f);
    }
}
