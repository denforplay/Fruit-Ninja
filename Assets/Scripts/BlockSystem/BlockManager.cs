using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;

    public readonly List<Block> allBlocks = new List<Block>();

    public void Remove(Block blockToRemove)
    {
        Fruit fruitToRemove = blockToRemove as Fruit;
        if (fruitToRemove != null && fruitToRemove.IsNotCutted)
        {
            _healthController.DeleteHeart();
        }

        allBlocks.Remove(blockToRemove);
    }

    public void Add(Block blockToAdd)
    {
        allBlocks.Add(blockToAdd);
    }
}
