using UnityEngine;
using DG.Tweening;

public abstract class Block : PhysicObject
{
    [SerializeField] protected float _radius;

    protected SpriteRenderer _spriteRenderer;

    public BlockManager _blockManager;

    protected bool _isNotCutted = true;

    protected IScalable _iScalable;

    protected IRotatable _iRotatable;

    public float Radius => _radius;
    public bool IsNotCutted => _isNotCutted;
    public SpriteRenderer GetSpriteRenderer => _spriteRenderer;
    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _iScalable = new NoScale();
        _iRotatable = new NoRotate();
        _iScalable.ScaleObject(this);
        _iRotatable.RotateObject(this);
    }

    public abstract void Cut();

    protected void OnBecameInvisible()
    {
        if (_blockManager!= null && _blockManager.allBlocks.Contains(this))
        {
            _blockManager.Remove(this);
        }

        Destroy(gameObject);
        DOTween.Kill(transform);
    }
}
