using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private SceneController _sceneController;

    public Action RemoveBlockEvent;

    public readonly List<Block> allBlocks = new List<Block>();

    public void RemoveHeartBonus(HeartBonus heartBonus)
    {
        allBlocks.Remove(heartBonus);
        if (!heartBonus.IsNotCutted )
        {
            _healthController.AddHeart();
        }
        else if (allBlocks.Count == 1 && _healthController.HeartsCount == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveFruit(Fruit fruit)
    {
        allBlocks.Remove(fruit);
        if (fruit.IsNotCutted)
        {
            _healthController.DeleteHeart();
        }
        else if (_healthController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveBomb(Bomb bomb)
    {
        allBlocks.Remove(bomb);
        if (!bomb.IsNotCutted)
        {
            _healthController.DeleteHeart();
        }
        else if (_healthController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void Add(Block blockToAdd)
    {
        allBlocks.Add(blockToAdd);
    }
}
