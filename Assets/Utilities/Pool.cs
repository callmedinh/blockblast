using System;
using System.Collections.Generic;
using Block;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class Pool : MonoBehaviour
    {
        private Dictionary<BlockType, Stack<GameObject>> _pool = new();
        [SerializeField] private List<PoolBlock> poolBlocks;
    

        private void Awake()
        {
            Intitialize();
        }

        private void Intitialize()
        {
            foreach (var block in poolBlocks)
            {
                var stack = new Stack<GameObject>();
                for (int i = 0; i < block.size; i++)
                {
                    var obj = Instantiate(block.prefab, this.transform, true);
                    obj.SetActive(false);
                    stack.Push(obj);
                }
                _pool.Add(block.type, stack);
            }
        }

        public GameObject GetPooledObject(BlockType blockType)
        {
            GameObject obj = null;
            if (_pool.TryGetValue(blockType, out var stack))
            {
                if (stack.Count > 0)
                {
                    obj = stack.Pop();
                    obj.SetActive(true);
                }
            }
            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            var identifier = obj.GetComponent<BlockTypeIdentifier>();
            if (identifier != null && _pool.TryGetValue(identifier.blockType, out var stack))
            {
                obj.SetActive(false);
                stack.Push(obj);
            }
            else
            {
                Debug.LogWarning($"BlockType not found for object {obj.name}");
            }
        }
        public BlockType GetRandomAvailableType()
        {
            var keys = new List<BlockType>(_pool.Keys);
            if (keys.Count == 0) return default;

            BlockType type = keys[Random.Range(0, keys.Count)];
            return type;
        }
    
    }

    [Serializable]
    public class PoolBlock
    {
        public BlockType type;
        public int size;
        public GameObject prefab;
    }
}