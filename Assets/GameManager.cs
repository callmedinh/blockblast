using System;
using DefaultNamespace.Utilities;
using UnityEngine;

namespace DefaultNamespace
{
    public enum GameState
    {
        GameOver, InGameplay
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private MapInfo mapInfo;
        public GameState CurrentGameState { get; private set; } = GameState.GameOver;

        private void Start()
        {
            InitInGamestate();
        }

        private void OnEnable()
        {
            GameEvent.OnStartGame += InitInGamestate;
            GameEvent.OnGameOver += ChangeGameState;
        }

        private void OnDisable()
        {
            GameEvent.OnStartGame -= InitInGamestate;
            GameEvent.OnGameOver -= ChangeGameState;
        }

        private void ChangeGameState()
        {
            CurrentGameState = GameState.GameOver;
        }
        private void InitInGamestate()
        {
            MapManager.Instance.InitMap(mapInfo);
            CameraController.Instance.SetPosition(mapInfo.mapSize);
            BlockManager.Instance.RandomInitBlock(3);
            CurrentGameState = GameState.InGameplay;
        }
    }
}