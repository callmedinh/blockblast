using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Utilities;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("UI Views")] 
        [SerializeField] private BaseUIView gameplayView;
        [SerializeField] private BaseUIView settingView;
        [SerializeField] private BaseUIView gameOverView;

        private Dictionary<string, BaseUIView> _uiScreen = new Dictionary<string, BaseUIView>();
        private BaseUIView _currentActiveScreen;
        private void Awake()
        {
            if (gameplayView != null) _uiScreen.Add(GameConstants.UIScreenGameplay, gameplayView);
            if (settingView != null) _uiScreen.Add(GameConstants.UIScreenSetting, settingView);
            if (gameOverView != null) _uiScreen.Add(GameConstants.UIScreenGameover, gameOverView);

            foreach (var screen in _uiScreen)
            {
                screen.Value.Hide();
            }
        }

        public void ShowScreen(string uiName)
        {
            if (_uiScreen.ContainsKey(uiName))
            {
                if (_currentActiveScreen != null)
                {
                    _currentActiveScreen.Hide();
                }
                _currentActiveScreen = _uiScreen[uiName];
                _currentActiveScreen.Show();
            }
            else
            {
                Debug.LogWarning($"UI {uiName} not found");
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
}

