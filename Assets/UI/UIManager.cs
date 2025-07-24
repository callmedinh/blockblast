using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("UI Views")] 
        [SerializeField] private BaseUIView gameplayView;
        [SerializeField] private BaseUIView settingView;
        [SerializeField] private BaseUIView gameOverView;

        private Dictionary<ScreenType, BaseUIView> _uiScreen = new();
        private BaseUIView _currentActiveScreen;
        private void Awake()
        {
            if (gameplayView != null) _uiScreen.Add(ScreenType.Gameplay, gameplayView);
            if (settingView != null) _uiScreen.Add(ScreenType.Setting, settingView);
            if (gameOverView != null) _uiScreen.Add(ScreenType.Gameover, gameOverView);

            foreach (var screen in _uiScreen)
            {
                screen.Value.Hide();
            }
        }

        public void ShowScreen(ScreenType type)
        {
            if (_uiScreen.ContainsKey(type))
            {
                if (_currentActiveScreen != null)
                {
                    HideUI();
                }
                _currentActiveScreen = _uiScreen[type];
                _currentActiveScreen.Show();
            }
            else
            {
                Debug.LogWarning($"UI {type} not found");
            }
        }
        public void HideUI()
        {
            if (_currentActiveScreen != null)
            {
                _currentActiveScreen.Hide();
                _currentActiveScreen = null;
            }
        }
    }
    public enum ScreenType
    {
        Gameplay,
        Setting,
        Gameover,
    }
}

