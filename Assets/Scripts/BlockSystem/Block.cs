using UnityEngine;
using DG.Tweening;

public class Block : PhysicObject
{
    [SerializeField] private float _radius = 2.0f;
    public BlockManager _blockManager;
    public float Radius => _radius;

    private void Awake()
    {
        base.Awake();
    }

    public void ScaleBlock()
    {
        this.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, _radius);
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

    protected void OnBecameInvisible()
    {
        if (_blockManager!= null && _blockManager.allBlocks.Contains(this))
        {
            _blockManager.Remove(this);
        }

        Destroy(this.gameObject);
        DOTween.Kill(transform);
    }
}
