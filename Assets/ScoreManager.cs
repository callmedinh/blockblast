using System;
using Block;
using Data;
using Events;

public class ScoreManager : Singleton<ScoreManager>
{
    public int CurrentScore { get; private set; }
    
    public event Action<int> OnScoreChanged;
    private void OnEnable()
    {
        GameEvent.OnBlockPlaced += AddScoreForBlockBlaced;
        GameEvent.OnLinesCleared += AddScoreForLinesCleared;
        GameEvent.OnGameStarted += ResetScore;
    }
    void AddScoreForBlockBlaced(BlockController block)
    {
        CurrentScore++;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    void AddScoreForLinesCleared()
    {
        CurrentScore += 9;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void HandleGameOver()
    {
        var entry  = new GameDataEntry(CurrentScore, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        var container = SaveLoadSystem.Instance.LoadGameData();
        container.gameDataList.Add(entry);
        SaveLoadSystem.Instance.SaveGameData(container);
    }

    void ResetScore()
    {
        CurrentScore = 0;
        OnScoreChanged?.Invoke(CurrentScore);
    }
}