
using System;
using Events;

namespace DefaultNamespace
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int CurrentScore { get; private set; }
        public int BestScore { get; private set; }

        //event relate to score
        public event Action<int> OnScoreChanged;
        private void OnEnable()
        {
            GameEvent.OnBlockPlaced += AddScoreForBlockBlaced;
            GameEvent.OnLinesCleared += AddScoreForLinesCleared;
            GameEvent.OnGameStarted += ResetScore;
            GameEvent.OnGameOver += CheckForBestScoreAndSave;
        }

        void CheckForBestScoreAndSave()
        {
            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
                GameEvent.OnBestScoreChanged?.Invoke(BestScore);
            }
        }
        void AddScoreForBlockBlaced()
        {
            CurrentScore++;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        void AddScoreForLinesCleared()
        {
            CurrentScore += 9;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        void ResetScore()
        {
            CurrentScore = 0;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}