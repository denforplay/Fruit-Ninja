using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Block
{
    private bool _isHeart = true;
    public bool IsHeart => _isHeart;

    private new void Start()
    {
        _iRotatable = new NoRotate();
        _iScalable = new NoScale();
        base.Start();
    }

    public override void Cut()
    {
        _isHeart = false;
    }

    public void SetHeartActive()
    {
        _isHeart = true;
    }
}
