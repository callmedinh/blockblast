using System.Collections.Generic;
using Events;
using Map;
using UnityEngine;
using Utilities;

namespace Block
{
    public class BlockSystem
    {
        private readonly Vector3[] _spawnPos = new Vector3[3]
        {
            new Vector3(0f, -2f, 0f),
            new Vector3(3f, -2f, 0f),
            new Vector3(8f, -2f, 0f),
        };

        public readonly List<BlockController> availableBlocks = new();
        private readonly Pool _pool;

        public BlockSystem(Pool pool)
        {
            _pool = pool;
        }
        public bool HasAvailableBlocks => availableBlocks.Count > 0;
        public void ClearAllActiveeBlock()
        {
            foreach (var block in availableBlocks)
            {
                _pool.ReturnToPool(block.gameObject);
            }

            availableBlocks.Clear();
        }

        public void HideActiveBlocks()
        {
            foreach (var block in availableBlocks)
            {
                block.gameObject.SetActive(false);
            }
        }

        public void ShowActiveBlocks()
        {
            foreach (var block in availableBlocks)
            {
                block.gameObject.SetActive(true);
            }
        }

        public void SpawnRandomBlocks(int size)
        {
            for (int i = 0; i < size; i++)
            {
                var randomType = _pool.GetRandomAvailableType();
                var obj = _pool.GetPooledObject(randomType);

                if (obj != null)
                {
                    obj.transform.position = _spawnPos[i];
                    var block = obj.GetComponent<BlockController>();
                    availableBlocks.Add(block);
                }
            }
        }

        public void RemoveBlock(BlockController blockController)
        {
            if (availableBlocks.Contains(blockController))
            {
                availableBlocks.Remove(blockController);
                _pool.ReturnToPool(blockController.gameObject);
            }
        }
    }
}
