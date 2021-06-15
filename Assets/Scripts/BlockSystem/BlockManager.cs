using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private SceneController _sceneController;
    public readonly List<Block> allBlocks = new List<Block>();
    [SerializeField] private float count;

    public void Remove(Block blockToRemove)
    {
        allBlocks.Remove(blockToRemove);
        count = allBlocks.Count;
        Fruit fruitToRemove = blockToRemove as Fruit;
        if (fruitToRemove != null && fruitToRemove.IsNotCutted)
        {
            _healthController.DeleteHeart();
        }

        Bomb bombToRemove = blockToRemove as Bomb;
        if (bombToRemove != null && !bombToRemove.IsNotCutted)
        {
            _healthController.DeleteHeart();
        }

    }

    public void Add(Block blockToAdd)
    {
        allBlocks.Add(blockToAdd);
        count = allBlocks.Count;
    }
}
