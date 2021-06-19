using UnityEngine;
using DG.Tweening;

public abstract class Block : PhysicObject
{
    [SerializeField] protected float _radius;
    [SerializeField] BlocksAnimationConfig _blockAnimationConfig; 
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
        int randomAnimation = Random.Range(_blockAnimationConfig.minAnimationCount, _blockAnimationConfig.maxAnimationCount);
        float duration = Random.Range(_blockAnimationConfig.startRandomDuration, _blockAnimationConfig.endRandomDuration);
        float scale = Random.Range(_blockAnimationConfig.startRandomScale, _blockAnimationConfig.endRandomScale);
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
        Destroy(gameObject);
        DOTween.Kill(transform);
    }
}
