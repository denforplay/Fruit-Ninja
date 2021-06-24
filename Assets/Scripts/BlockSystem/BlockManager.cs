using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private HealthViewController _healthViewController;
    [SerializeField] private SceneController _sceneController;

    public Action RemoveBlockEvent;

    public readonly List<Block> allBlocks = new List<Block>();

    public void RemoveMagnitBonus(MagnitBonus magnitBonus)
    {
        allBlocks.Remove(magnitBonus);

        if (_healthViewController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveHeartBonus(HeartBonus heartBonus)
    {
        allBlocks.Remove(heartBonus);
        if (!heartBonus.IsNotCutted )
        {
            _healthViewController.AddHeart();
        }
        else if (_healthViewController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveFruit(Fruit fruit)
    {
        allBlocks.Remove(fruit);
        if (fruit.IsNotCutted)
        {
            _healthViewController.DeleteHeart();
        }
        else if (_healthViewController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveBomb(Bomb bomb)
    {
        allBlocks.Remove(bomb);
        if (!bomb.IsNotCutted)
        {
            _healthViewController.DeleteHeart();
        }
        else if (_healthViewController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void RemoveFreezeBonus(FreezeBonus freezeBonus)
    {
        allBlocks.Remove(freezeBonus);

        if (_healthViewController.HeartsCount == 0 && allBlocks.Count == 0)
        {
            _sceneController.PopUpRestart();
        }
    }

    public void Add(Block blockToAdd)
    {
        allBlocks.Add(blockToAdd);
    }
}
