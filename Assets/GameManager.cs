
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
        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        private void Awake()
        {
            LoadData();
        }

        private void Start()
        {
            StartGameplay();
        }

        private void OnEnable()
        {
            GameEvent.OnGameStarted += StartGameplay;
            GameEvent.OnGameOver += GameOver;
            GameEvent.OnBlockPlaced += HandleBlockPlaced;
            GameEvent.OnLinesCleared += HandleLinesCleared;
        }

        private void OnDisable()
        {
            GameEvent.OnGameStarted -= StartGameplay;
            GameEvent.OnGameOver -= GameOver;
            GameEvent.OnBlockPlaced -= HandleBlockPlaced;
            GameEvent.OnLinesCleared -= HandleLinesCleared;
        }
        
        public void StartGameplay()
        {
            CurrentScore = 0;
            LoadData();
            UIManager.Instance.ShowGameplayUI(CurrentScore, BestScore);
            MapManager.Instance.InitMap(mapInfo);
            CurrentGameState = GameState.InGameplay;
            //BlockManager.Instance.SpawnRandomBlocks(3);
        }

        public void HandleBlockPlaced()
        {
            CurrentScore++;
            UIManager.Instance.UpdateScore(CurrentScore, BestScore);
        }

        public void HandleLinesCleared()
        {
            CurrentScore += 9;
            UIManager.Instance.UpdateScore(CurrentScore, BestScore);
        }
        public void GameOver()
        {
            if (CurrentScore > BestScore)
            {
                SaveData();
            }
            UIManager.Instance.ShowGameOverUI(CurrentScore, BestScore);
            CurrentGameState = GameState.GameOver;
        }

        void SaveData()
        {
            SaveLoadSystem.Instance.Save(new DataUser(CurrentScore));
        }

        void LoadData()
        {
            var data = SaveLoadSystem.Instance.Load();
            BestScore = data.grade;
        }
    }
}