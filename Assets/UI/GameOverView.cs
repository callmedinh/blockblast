using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : BaseUIView
{
    [SerializeField] private Button playButon;
    [SerializeField] private TMP_Text scoreText;
    
    //event
    public event Action OnPlayButtonClicked;

    private void Awake()
    {
        playButon.onClick.AddListener(OnPlayButtonClick);
    }

    void OnPlayButtonClick()
    {
        OnPlayButtonClicked?.Invoke();
    }

    public void DislayScoreText(int value)
    {
        scoreText.text = value.ToString();
    }
}
