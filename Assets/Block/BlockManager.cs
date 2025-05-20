using System;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockManager : Singleton<BlockManager>
{
    [SerializeField] private Pooler[] pooler;
    [SerializeField] private Transform[] initPosition;
    public static int BlockCount = 3;

    private void Update()
    {
        if (BlockCount == 0)
        {
            BlockCount = 3;
            RandomInitBlock(BlockCount);
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
    }
}
