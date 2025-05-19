using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    private Stack<GameObject> _stack = new Stack<GameObject>();
    [SerializeField] private int size;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        InitPooler();
    }

    public void InitPooler()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, this.transform, true);
            obj.SetActive(false);
            _stack.Push(obj);
        }
    }
    public GameObject GetPooler()
    {
        GameObject obj;
        if (_stack.Count > 0)
        {
            obj = _stack.Pop();
        }
        else
        {
            obj = Instantiate(prefab, this.transform, true);
        }
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        _stack.Push(obj);
    }

}
