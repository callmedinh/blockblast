
using System.Collections.Generic;
using DefaultNamespace;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockManager : Singleton<BlockManager>
{
    [SerializeField] private Pooler[] pooler;

    private Vector3[] ComputerPos = new Vector3[3]
    {
        new Vector3(-3f, 1f, 0f),
        new Vector3(-3f, 4f, 0f),
        new Vector3(-3f, 7f, 0f),
    };

    private Vector3[] HandheldPos = new Vector3[3]
    {
        new Vector3(0f, -2f, 0f),
        new Vector3(3f, -2f, 0f),
        new Vector3(8f, -2f, 0f),
    };

    private Vector3[] _spawnPos;
    public List<Block> availableBlocks = new List<Block>();

    private void Awake()
    {
        DetermineSpawnPosition();
    }

    private void Update()
    {
        if (availableBlocks.Count == 0 && GameManager.Instance.CurrentGameState == GameState.InGameplay)
        {
            SpawnRandomBlocks(3);
        }

        if (!MapManager.Instance.CanPlaceAnyBlocks(availableBlocks))
        {
            GameEvent.OnGameOver?.Invoke();
            foreach (var block in availableBlocks)
            {
                block.GetComponentInParent<Pooler>().ReturnToPool(block.gameObject);
            }
            availableBlocks.Clear();
        }
    }

    private void DetermineSpawnPosition()
    {
        _spawnPos = HandheldPos;
    }
    
    public void SpawnRandomBlocks(int size)
    {
        for (int i = 0; i < size; i++)
        {
            int index = Random.Range(0, pooler.Length);
            if (pooler == null) Debug.LogError("NUll");
            PlaceBlockAtInitPosition(i, pooler[index].GetPooler());
        }
    }

    public void PlaceBlockAtInitPosition(int i, GameObject prefab)
    {
        prefab.transform.position = _spawnPos[i];
        availableBlocks.Add(prefab.GetComponent<Block>());
    }

    public void RemoveBlock(Block block)
    {
        if (availableBlocks.Contains(block))
        {
            availableBlocks.Remove(block);
        }
    }
}
