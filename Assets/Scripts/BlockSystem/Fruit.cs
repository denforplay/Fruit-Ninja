using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Block
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        if (!_spriteRenderer.isVisible)
        {
            Destroy(this.gameObject);
        }

        RotateBlock();
        ScaleBlock();
    }
}
