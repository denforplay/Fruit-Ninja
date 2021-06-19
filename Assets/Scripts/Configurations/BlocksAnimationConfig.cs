using System;

[Serializable]
public class BlocksAnimationConfig
{
    public float startRandomDuration = 4.0f;
    public float endRandomDuration = 6.0f;
    public float startRandomScale = 0.5f;
    public float endRandomScale = 1.2f;
    public int minAnimationCount = 1;
    public int maxAnimationCount = 4;
}
