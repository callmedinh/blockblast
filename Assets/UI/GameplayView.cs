using System;
using DefaultNamespace;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameplayView : BaseUIView
    {
        [SerializeField] private Button settingButton;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        Timer _timer;

        private void OnEnable()
        {
            _timer = new Timer(timerText);
            settingButton.onClick.AddListener(HandleSettingButtonClick);
            ScoreManager.Instance.OnScoreChanged += DisplayScore;
        }

        private void Update()
        {
            _timer.DisplayTimer();
        }

        private void OnDisable()
        {
            settingButton.onClick.RemoveListener(HandleSettingButtonClick);
        }

        private void DisplayScore(int score)
        {
            scoreText.text = score.ToString();
        }
        void HandleSettingButtonClick()
        {
            GameManager.Instance.ChangeState(GameState.Setting);
        }
    }
}