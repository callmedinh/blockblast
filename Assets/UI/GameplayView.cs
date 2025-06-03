using System;
using DefaultNamespace;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class GameplayView : BaseUIView
    {
        [SerializeField] private Button settingButton;
        [SerializeField] private TMP_Text scoreText;

        //event action
        public event Action OnSettingButtonClicked;
        private void Awake()
        {
            settingButton.onClick.AddListener(() => OnSettingButtonClicked?.Invoke());
        }

        public void DisplayScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}