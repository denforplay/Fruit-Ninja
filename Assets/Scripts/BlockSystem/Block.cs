using UnityEngine;
using DG.Tweening;

public class Block : PhysicObject
{
    public void ScaleBlock()
    {
        this.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 10f);
    }

    public void RotateBlock()
    {
        this.transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360);
    }
}
