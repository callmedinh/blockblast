
using DefaultNamespace;
using Events;
using Utilities;

namespace UI
{
    public class GameplayPresenter
    {
        GameplayView _view;
        public GameplayPresenter(GameplayView view, ScoreManager scoreManager)
        {
            _view = view;
            _view.OnSettingButtonClicked += SettingButtonClickedHandler;
            scoreManager.OnScoreChanged += ScoreChangedHandler;
        }

        void SettingButtonClickedHandler()
        {
            UIManager.Instance.ShowScreen(GameConstants.UIScreenSetting);
        }
        void ScoreChangedHandler(int score)
        {
            _view.DisplayScore(score);
        }
    }
}