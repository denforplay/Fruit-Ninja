using UnityEngine;

public class SpawnerLine : MonoBehaviour
{
    [SerializeField] private int _priority;
    [SerializeField] private GameObject _firstLinePoint;
    [SerializeField] private GameObject _secondLinePoint;

    public int Priority => _priority;
    private Vector3 FindPointToDrop()
    {
        Vector3 pointToDrop = new Vector3();
        pointToDrop.x = Random.Range(_firstLinePoint.transform.position.x, _secondLinePoint.transform.position.x);
        pointToDrop.y = Random.Range(_firstLinePoint.transform.position.y, _secondLinePoint.transform.position.y);
        return pointToDrop;
    }

    public Block GenerateDroppingBlock(Block blockPrefab)
    {
        Vector3 spawnPosition = FindPointToDrop();

        Block droppingBlock = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        BlockManager._allBlocks.Add(droppingBlock);
        return droppingBlock;
    }

}
