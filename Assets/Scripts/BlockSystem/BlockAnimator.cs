using UnityEngine;

public class BlockAnimator
{
    protected IScalable _iScalable = new Scale();
    protected IRotatable _iRotatable = new Rotate();
    BlocksAnimationConfig _blockAnimationConfig = new BlocksAnimationConfig();

    public bool IsAnimated = false;

    public void RandomAnimate(Block block)
    {
        if (IsAnimated)
        {
            int randomAnimation = Random.Range(_blockAnimationConfig.minAnimationCount, _blockAnimationConfig.maxAnimationCount);
            float duration = Random.Range(_blockAnimationConfig.startRandomDuration, _blockAnimationConfig.endRandomDuration);
            float scale = Random.Range(_blockAnimationConfig.startRandomScale, _blockAnimationConfig.endRandomScale);
            switch (randomAnimation)
            {
                case 1:
                    {
                        _iScalable.ScaleObject(block, duration, scale);
                    }
                    break;
                case 2:
                    {
                        _iRotatable.RotateObject(block, duration);
                    }
                    break;
                case 3:
                    {
                        _iScalable.ScaleObject(block, duration, scale);
                        _iRotatable.RotateObject(block, duration);
                    }
                    break;
                case 4:
                    break;
            }
        }
    }

    public void SetRotatable(IRotatable rotatable)
    {
        _iRotatable = rotatable;
    }

    public void SetScalable(IScalable scalable)
    {
        _iScalable = scalable;
    }
}