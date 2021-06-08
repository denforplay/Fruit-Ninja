using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<SpawnerLine> _spawnerLines;
    [SerializeField] private Block _blockPrefab;

    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 4f;

    [SerializeField] private int _minBlocks = 2;
    [SerializeField] private int _maxBlocks = 5;

    [SerializeField] private float _minInterval = 2.5f;
    [SerializeField] private float _maxInterval = 5.0f;


    private Coroutine _spawnerCoroutine;
    private List<SpawnerLine> _priorityList = new List<SpawnerLine>();

    private List<Block> currentBlocks = new List<Block>();
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
    private void Start()
    {
        FillPriority();
        StartSpawn();
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            StartCoroutine(SpawnBlockPackage(_priorityList[Random.Range(0, _priorityList.Count - 1)], Random.Range(_minBlocks, _maxBlocks)));
            yield return new WaitForSeconds(_minInterval);
        }
    }

    private IEnumerator SpawnBlockPackage(SpawnerLine spawnerLine, int blocksCount)
    {
        currentBlocks = new List<Block>();
        while (blocksCount > 0)
        {
            Block block = spawnerLine.GenerateDroppingBlock(_blockPrefab);
            float horizontalSpeed = Random.Range(_minSpeed, _maxSpeed);
            float verticalSpeed = horizontalSpeed * Random.Range(1f, 1.5f);
            block.AddSpeed(new Vector3(horizontalSpeed, verticalSpeed));
            if (block.transform.position.x > Camera.main.transform.position.x && block.GetSpeed().x > 0)
            {
                block.ReverseHorizontalSpeed();
            }

            blocksCount--;
        }

        yield return new WaitForSeconds(_minInterval);
    }

    private void StartSpawn()
    {
        _spawnerCoroutine = StartCoroutine(Spawn());
    }
}
