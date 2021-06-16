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
        int randomAnimation = Random.Range(1,4);
        float duration = Random.Range(4.0f, 6.0f);
        float scale = Random.Range(0.5f, 1.5f);
        switch (randomAnimation)
        {
            case 1:
                {
                    _iScalable.ScaleObject(this, duration, scale);
                }
                break;
            case 2:
                {
                    _iRotatable.RotateObject(this, duration);
                }
                break;
            case 3:
                {
                    _iScalable.ScaleObject(this, duration, scale);
                    _iRotatable.RotateObject(this, duration);
                }
                break;
            case 4:
                break;
        }
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
