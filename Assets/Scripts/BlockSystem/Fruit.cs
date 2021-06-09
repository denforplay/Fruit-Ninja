using UnityEngine;
using DG.Tweening;
public class Fruit : Block
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        base.Update();
        RotateBlock();
        ScaleBlock();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        DOTween.Kill(transform);
    }
}
