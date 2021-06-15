using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Block
{
    private bool _isHeart = true;
    public bool IsHeart => _isHeart;

    public override void Cut()
    {
        _isHeart = false;
    }
}
