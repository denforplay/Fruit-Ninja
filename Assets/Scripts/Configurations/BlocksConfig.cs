using System;
using System.Collections.Generic;

[Serializable]
public class BlocksConfig 
{
    public List<Block> blockPrefab;
    public List<Block> bonusBlocks;
    public List<float> bonusChances;

    public int minBlocksPackage = 2;
    public int maxBlocksPackage = 5;
    public float minPackageInterval = 2.5f;
    public float maxPackageInterval = 5.0f;
    public float minBlockInterval = 0.2f;
    public float maxBlockInterval = 0.5f;
    public float heartSpeed = 1.2f;
}
