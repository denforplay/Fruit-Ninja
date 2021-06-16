using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerLine> _spawnerLines;

    [SerializeField] private SpeedConfig _speedConfig;

    [SerializeField] private BlocksConfig _blocksConfig;

    [SerializeField] private float _difficulty = 0;

    [SerializeField] private BlockManager _blockManager;

    [SerializeField] private HealthController _healthController;

    [SerializeField] private Cutting _cutting;

    [SerializeField] private Player _player;

    [SerializeField] private Bomb _bomb;

    [SerializeField] private HeartBonus _heartBonus;

    [SerializeField] private float _bombChance = 0.5f;
    [SerializeField] private float _heartBonusChance = 0.5f;

    private float _maxDifficulty = 100f;

    private Coroutine _spawnerCoroutine;
    private List<SpawnerLine> _priorityList = new List<SpawnerLine>();
    private void Start()
    {
        FillPriority();
        StartSpawn();
    }

    private void FillPriority()
    {
        for (int i = 0; i < _spawnerLines.Count; i++)
        {
            for (int j = 0; j < _spawnerLines.Count - _spawnerLines[i].Priority; j++)
            {
                _priorityList.Add(_spawnerLines[i]);
            }
        }
    }

    public float FindValueForCurrentDifficulty(float min, float max)
    {
        if (_difficulty <= _maxDifficulty)
        {
            float difficultPercentage = _difficulty / _maxDifficulty;

            float additionToMinByDifficulty = difficultPercentage * (max - min);

            return additionToMinByDifficulty + min;
        }
        else
        {
            return max;
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            int numberOfBlocks = (int)FindValueForCurrentDifficulty(_blocksConfig.minBlocksPackage, _blocksConfig.maxBlocksPackage);
            StartCoroutine(SpawnBlockPackage(_priorityList[Random.Range(0, _priorityList.Count - 1)], numberOfBlocks));
            _difficulty++;
            float secondsBetweenPackages = FindValueForCurrentDifficulty(_blocksConfig.maxPackageInterval, _blocksConfig.maxPackageInterval);
            yield return new WaitForSeconds(secondsBetweenPackages);
        }
    }

    private IEnumerator SpawnBlockPackage(SpawnerLine spawnerLine, int blocksCount)
    {
        int bombsCount = Random.Range(1, blocksCount / 2);
        int heartBonusCount = Random.Range(1, blocksCount / 2);
        while (blocksCount > 0)
        {
            Block block;
            float bombRand = Random.Range(0f, 1f);
            float heartRand = Random.Range(0f, 1f);
            if (bombsCount > 0 && bombRand <= _bombChance)
            {
                block = spawnerLine.GenerateDroppingBlock(_bomb);
                bombsCount--;
            }
            else if (heartBonusCount > 0 && heartRand <= _heartBonusChance && _player.health < _player.maxhealth)
            {
                block = spawnerLine.GenerateDroppingBlock(_heartBonus);
                heartBonusCount--;
            }
            else
            {
                Block randomFruit = _blocksConfig.blockPrefab[Random.Range(0, _blocksConfig.blockPrefab.Count)];
                block = spawnerLine.GenerateDroppingBlock(randomFruit);
            }
           
            float horizontalSpeed = FindValueForCurrentDifficulty(_speedConfig.speedMin, _speedConfig.speedMax);
            float verticalSpeed = horizontalSpeed * Random.Range(1f, 1.5f);
            block.AddSpeed(new Vector3(horizontalSpeed, verticalSpeed));
            if (block.transform.position.x > Camera.main.transform.position.x && block.GetSpeed().x > 0)
            {
                block.ReverseHorizontalSpeed();
            }

            blocksCount--;
            float secondsToWaitBetweenBlocks = FindValueForCurrentDifficulty(_blocksConfig.maxBlockInterval, _blocksConfig.minBlockInterval);
            yield return new WaitForSeconds(secondsToWaitBetweenBlocks);
        }

    }

    public void StartSpawn()
    {
        _player.health = _player.maxhealth;
        _player.score = 0;
        _cutting.StartGame();
        _healthController.InstantiateHearts();
        _spawnerCoroutine = StartCoroutine(Spawn());
    }

    public void StopSpawn()
    {
        StopCoroutine(_spawnerCoroutine);
    }
}
