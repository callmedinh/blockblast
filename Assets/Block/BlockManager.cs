using System;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockManager : Singleton<BlockManager>
{
    [SerializeField] private Pooler poolerL;
    [SerializeField] private Pooler poolerReverseL;
    [SerializeField] private Pooler poolerSquare;
    [SerializeField] private Pooler poolerDot;
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
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    InitPositionBlock(i, poolerL.GetPooler());
                    break;
                case 1: 
                    InitPositionBlock(i, poolerReverseL.GetPooler());
                    break;
                case 2:
                    InitPositionBlock(i, poolerSquare.GetPooler());
                    break;
                case 3:
                    InitPositionBlock(i, poolerDot.GetPooler());
                    break;
            }
        }
    }

    public void InitPositionBlock(int i, GameObject prefab)
    {
        prefab.transform.position = initPosition[i].position;
    }
}
