using UnityEngine;
using DG.Tweening;
public class Fruit : Block
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
}
