using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerLine> _spawnerLines;
    [SerializeField] private List<Block> _blockPrefab;

    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 4f;

    [SerializeField] private int _minBlocksPackage = 2;
    [SerializeField] private int _maxBlocksPackage = 5;

    [SerializeField] private float _minPackageInterval = 2.5f;
    [SerializeField] private float _maxPackageInterval = 5.0f;

    [SerializeField] private float _minBlockInterval = 0.2f;
    [SerializeField] private float _maxBlockInterval = 0.5f;


    [SerializeField] private float _difficulty = 0;
    private float _maxDifficulty = 100f;
    private Coroutine _spawnerCoroutine;
    private List<SpawnerLine> _priorityList = new List<SpawnerLine>();

    private List<Block> currentBlocks = new List<Block>();

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
        if (_difficulty <= 100)
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
            int numberOfBlocks = (int)FindValueForCurrentDifficulty(_minBlocksPackage, _maxBlocksPackage);
            StartCoroutine(SpawnBlockPackage(_priorityList[Random.Range(0, _priorityList.Count - 1)], numberOfBlocks));
            _difficulty++;
            yield return new WaitForSeconds(_maxPackageInterval);
        }
    }

    private IEnumerator SpawnBlockPackage(SpawnerLine spawnerLine, int blocksCount)
    {
        currentBlocks = new List<Block>();
        while (blocksCount > 0)
        {
            Block block = spawnerLine.GenerateDroppingBlock(_blockPrefab[Random.Range(0, _blockPrefab.Count)]);
            float horizontalSpeed = FindValueForCurrentDifficulty(_minSpeed, _maxSpeed);
            float verticalSpeed = horizontalSpeed * Random.Range(1f, 1.5f);
            block.AddSpeed(new Vector3(horizontalSpeed, verticalSpeed));
            if (block.transform.position.x > Camera.main.transform.position.x && block.GetSpeed().x > 0)
            {
                block.ReverseHorizontalSpeed();
            }

            blocksCount--;
            float secondsToWaitBetweenBlocks = FindValueForCurrentDifficulty(_maxBlockInterval, _minBlockInterval);
            yield return new WaitForSeconds(secondsToWaitBetweenBlocks);
        }

    }

    private void StartSpawn()
    {
        _spawnerCoroutine = StartCoroutine(Spawn());
    }
}
