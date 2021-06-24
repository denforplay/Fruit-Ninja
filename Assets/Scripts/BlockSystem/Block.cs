using UnityEngine;
using DG.Tweening;

public abstract class Block : PhysicObject
{
    [SerializeField] protected float _radius;
    [SerializeField] private int _scoreCost;

    protected SpriteRenderer _spriteRenderer;
    protected bool _isNotCutted = true;
    protected BlockAnimator _blockAnimator = new BlockAnimator();

    public BlockManager _blockManager;
    public float Radius => _radius;
    public bool IsNotCutted => _isNotCutted;
    public SpriteRenderer GetSpriteRenderer => _spriteRenderer;
    public bool isSlowed = false;
    public int GetCost => _scoreCost;

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
