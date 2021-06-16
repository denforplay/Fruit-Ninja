using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private SceneController _sceneController;
    public readonly List<Block> allBlocks = new List<Block>();

    public void Remove(Block blockToRemove)
    {
        allBlocks.Remove(blockToRemove);
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

        HeartBonus heartBonusToRemove = blockToRemove as HeartBonus;
        if (heartBonusToRemove != null && !heartBonusToRemove.IsNotCutted)
        {
            _healthController.AddHeart();
        }
    }

    public void Add(Block blockToAdd)
    {
        allBlocks.Add(blockToAdd);
    }
}
