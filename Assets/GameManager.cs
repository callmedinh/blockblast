using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private MapInfo mapInfo;

        private void Start()
        {
            MapManager.Instance.InitMap(mapInfo);
            CameraController.Instance.SetPosition(mapInfo.mapSize);
            BlockManager.Instance.RandomInitBlock(3);
        }
    }
}