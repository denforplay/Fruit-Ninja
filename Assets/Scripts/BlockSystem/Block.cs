using UnityEngine;
using DG.Tweening;

public abstract class Block : PhysicObject
{
    [SerializeField] protected float _radius;
    protected SpriteRenderer _spriteRenderer;

    public BlockManager _blockManager;

    protected bool _isNotCutted = true;

    protected BlockAnimator _blockAnimator = new BlockAnimator();
    public float Radius => _radius;
    public bool IsNotCutted => _isNotCutted;
    public SpriteRenderer GetSpriteRenderer => _spriteRenderer;
    
    protected void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _blockAnimator.RandomAnimate(this);
    }

    public abstract void Cut();

    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
        DOTween.Kill(transform);
    }
}
