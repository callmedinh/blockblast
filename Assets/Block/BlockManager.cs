
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockManager : Singleton<BlockManager>
{
    [SerializeField] private Pooler[] pooler;
    [SerializeField] private Transform[] initPosition;
    public List<Block> availableBlocks = new List<Block>();

    private void Update()
    {
        if (availableBlocks.Count == 0 && GameManager.Instance.CurrentGameState == GameState.InGameplay)
        {
            RandomInitBlock(3);
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
    
    public void RandomInitBlock(int size)
    {
        for (int i = 0; i < size; i++)
        {
            int index = Random.Range(0, pooler.Length);
            InitPositionBlock(i, pooler[index].GetPooler());
        }
    }

    public void InitPositionBlock(int i, GameObject prefab)
    {
        prefab.transform.position = initPosition[i].position;
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
