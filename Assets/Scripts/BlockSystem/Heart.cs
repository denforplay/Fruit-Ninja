using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Block
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _isHeart = true;
    public bool IsHeart => _isHeart;
    public SpriteRenderer GetSpriteRenderer => _spriteRenderer;
    private void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
    }

    public void DeleteHeart()
    {
        _isHeart = false;
    }
}
