
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text gameplayScoreText;
    [SerializeField] private TMP_Text gameplayBestScoreText;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private TMP_Text gameoverScoreText;
    [SerializeField] private TMP_Text gameoverBestScoreText;
    [SerializeField] private Button playButton;
    public int currentGrade;
    public int maxGrade;
    private void OnEnable()
    {
        playButton.onClick.AddListener(() => GameManager.Instance.StartGameplay()); 
    }
    private void OnDisable()
    {
        playButton.onClick.RemoveListener(() => GameManager.Instance.StartGameplay());
    }
    public void UpdateScore(int currentScore, int bestScore)
    {
        gameplayScoreText.text = $"Current Grade: {currentScore}";
        if (currentGrade > maxGrade)
        {
            maxGrade = currentGrade;
            gameplayBestScoreText.text = $"Max Grade: {bestScore}";
        }
    }

    public void ShowGameplayUI(int currentScore, int bestScore)
    {
        gameplayScoreText.text = $"Current Grade: {currentScore}";
        gameplayBestScoreText.text = $"Max Grade: {bestScore}";
        gameoverPanel.SetActive(false);
        gameplayPanel.SetActive(true);
    }
    
    public void ShowGameOverUI(int currentScore, int bestScore)
    {
        gameoverPanel.SetActive(true);
        gameoverScoreText.text = currentScore.ToString();
        gameoverBestScoreText.text = bestScore.ToString();
        gameplayPanel.SetActive(false);
    }
}
