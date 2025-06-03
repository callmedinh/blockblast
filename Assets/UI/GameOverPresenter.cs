using DefaultNamespace;
using Events;
using Utilities;

namespace UI
{
    public class GameOverPresenter
    {
        private GameOverView _view;
        private ScoreManager _scoreManager;
        public GameOverPresenter(GameOverView view, ScoreManager scoreManager)
        {
            _view = view;
            _scoreManager = scoreManager;
            _view.OnPlayButtonClicked += OnPlayButtonHandler;
            GameEvent.OnGameOver += OnGameoverHandler;
        }

        void OnPlayButtonHandler()
        {
            GameEvent.OnGameStarted?.Invoke();
            UIManager.Instance.ShowScreen(GameConstants.UIScreenGameplay);
        }

        void OnGameoverHandler()
        {
            _view.DislayScoreText(_scoreManager.CurrentScore);
        }
    }
}