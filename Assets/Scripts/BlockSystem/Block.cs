using UnityEngine;
using DG.Tweening;

public class Block : PhysicObject
{
    [SerializeField] private float _radius = 1.0f;

    public float Radius => _radius;

    private bool isSlice;

    public void ScaleBlock()
    {
        this.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 10f);
    }

    public void RotateBlock()
    {
        this.transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360);
    }

    private void Update()
    {
        base.Update();
        RotateBlock();
        ScaleBlock();
    }

    public void Slice()
    {
        isSlice = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        
        DOTween.Kill(transform);
    }

    private void OnDestroy()
    {
        BlockManager._allBlocks.Remove(this);
    }
}
