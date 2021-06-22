using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerLine> _spawnerLines;
    [SerializeField] private SpeedConfig _speedConfig;
    [SerializeField] private BlockChanceConfig _blockChanceConfig;
    [SerializeField] private BlocksConfig _blocksConfig;
    [SerializeField] private BlockManager _blockManager;
    [SerializeField] private HealthViewController _healthViewController;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private Player _player;
    [SerializeField] private float _difficulty = 0;
    private float _maxDifficulty = 100f;
    private Coroutine _spawnerCoroutine;


    public void StartGame()
    {
        StartSpawn();
    }

    private SpawnerLine GetSpawnerLineByPriority()
    {
        int allLinePrioritySum = _spawnerLines.Sum(line => line.Priority);
        int zoneRandomer = Random.Range(0, allLinePrioritySum);
        int currentPriority = 0;
        for (int i = 0; i < _spawnerLines.Count; i++)
        {
            if (zoneRandomer <= currentPriority + _spawnerLines[i].Priority)
            {
                return _spawnerLines[i];
            }
            else
            {
                currentPriority += _spawnerLines[i].Priority;
            }
        }

        return _spawnerLines.Last();
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
            SpawnerLine _spawnerLine = GetSpawnerLineByPriority();
            StartCoroutine(SpawnBlockPackage(_spawnerLine, numberOfBlocks));
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
            if (bombsCount > 0 && bombRand <= _blockChanceConfig._bombChance)
            {
                block = spawnerLine.GenerateDroppingBlock(_blocksConfig.bomb);
                bombsCount--;
            }
            else if (heartBonusCount > 0 && heartRand <= _blockChanceConfig._heartBonusChance && _player.health < _player.maxhealth)
            {
                block = spawnerLine.GenerateDroppingBlock(_blocksConfig.heartBonus);
                heartBonusCount--;
            }
            else
            {
                Block randomFruit = _blocksConfig.blockPrefab[Random.Range(0, _blocksConfig.blockPrefab.Count)];
                block = spawnerLine.GenerateDroppingBlock(randomFruit);
            }
           
            float horizontalSpeed = FindValueForCurrentDifficulty(_speedConfig.speedMin, _speedConfig.speedMax);
            float verticalSpeed = horizontalSpeed * Random.Range(1f, 1.5f);
            if (block is HeartBonus)
            {
                Vector3 heartSpeed = new Vector3(horizontalSpeed * _blocksConfig.heartSpeed, verticalSpeed * _blocksConfig.heartSpeed);
                block.AddSpeed(heartSpeed);
            }
            else
            {
                block.AddSpeed(new Vector3(horizontalSpeed, verticalSpeed));
            }

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
        _healthViewController.InstantiateHearts();
        _spawnerCoroutine = StartCoroutine(Spawn());
    }

    public void StopSpawn()
    {
        StopCoroutine(_spawnerCoroutine);
    }
}
