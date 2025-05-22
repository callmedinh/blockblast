using System;
using DefaultNamespace;
using DefaultNamespace.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text currentGradeText;
    [SerializeField] private TMP_Text maxGradeText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text gameoverScoreText;
    [SerializeField] private TMP_Text gameoverBestScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject inGamePanel;
    public int currentGrade;
    public int maxGrade;

    private void Awake()
    {
        OnGameStarted();
    }
    private void OnEnable()
    {
        GameEvent.OnGameOver += HandleGameOver;
        GameEvent.OnLinesCleared += HandleLinesCleared;
        GameEvent.OnBlockPlaced += HandlePlacedBlock;
        playButton.onClick.AddListener(OnGameStarted); 
    }
    private void OnDisable()
    {
        GameEvent.OnGameOver -= HandleGameOver;
        GameEvent.OnLinesCleared -= HandleLinesCleared;
        GameEvent.OnBlockPlaced -= HandlePlacedBlock;
        playButton.onClick.RemoveListener(OnGameStarted);
    }

    private void HandlePlacedBlock()
    {
        currentGrade += 1;
        UpdateCurrentGrade();
    }

    private void HandleLinesCleared()
    {
        currentGrade += 9;
        UpdateCurrentGrade();
    }
    private void UpdateCurrentGrade()
    {
        currentGradeText.text = $"Current Grade: {currentGrade}";
        if (currentGrade > maxGrade)
        {
            maxGrade = currentGrade;
            maxGradeText.text = $"Max Grade: {maxGrade}";
        }
    }

    private void OnGameStarted()
    {
        DataUser dataUser = SaveLoadSystem.Instance.Load();
        Debug.Log(dataUser.grade.ToString());
        currentGrade = 0;
        maxGrade = dataUser.grade;
        currentGradeText.text = $"Current Grade: {currentGrade}";
        maxGradeText.text = $"Max Grade: {maxGrade}";
        gameOverPanel.SetActive(false);
        GameEvent.OnStartGame?.Invoke();
        inGamePanel.SetActive(true);
    }
    
    private void HandleGameOver()
    {
        if (currentGrade == maxGrade)
        {
            SaveLoadSystem.Instance.Save(new DataUser(maxGrade));
        }
        gameOverPanel.SetActive(true);
        gameoverScoreText.text = currentGrade.ToString();
        gameoverBestScoreText.text = maxGrade.ToString();
        inGamePanel.SetActive(false);
    }
}
