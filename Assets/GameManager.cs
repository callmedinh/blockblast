using Events;
using UI;
using UnityEngine;
using Utilities;

namespace DefaultNamespace
{
    public enum GameState
    {
        GameOver, InGameplay, Setting
    }
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private MapInfo mapInfo;
        public GameState CurrentGameState { get; private set; } = GameState.GameOver;
        

        private void Start()
        {
            OnGameStartedHandler();
        }

        private void OnEnable()
        {
            GameEvent.OnGameStarted += OnGameStartedHandler;
            GameEvent.OnGameOver += OnGameOverHandler;
        }

        private void OnDisable()
        {
            GameEvent.OnGameStarted -= OnGameStartedHandler;
            GameEvent.OnGameOver -= OnGameOverHandler;
        }
        
        private void OnGameStartedHandler()
        {
            MapManager.Instance.InitMap(mapInfo);
            CurrentGameState = GameState.InGameplay;
            UIManager.Instance.ShowScreen(GameConstants.UIScreenGameplay);
        }

        private void OnGameOverHandler()
        {
            if (CurrentGameState != GameState.GameOver)
            {
                CurrentGameState = GameState.GameOver;
                UIManager.Instance.ShowScreen(GameConstants.UIScreenGameover);
            }
        }
    }
}