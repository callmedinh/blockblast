using Audio;
using Block;
using Events;
using Map;
using UI;
using UnityEngine;

namespace Core
{
    public class GameOverState : IGameState
    {
        private BlockSystem _blockSystem;
        public GameOverState(BlockSystem blockSystem)
        {
            _blockSystem = blockSystem;
        }
        public void Enter()
        {
            ScoreManager.Instance.HandleGameOver();
            _blockSystem.ClearAllActiveeBlock();
            MapManager.Instance.ResetMap();
            UIManager.Instance.ShowScreen(ScreenType.Gameover);
            SoundManager.Instance.Play(State.GameOver);
            GameEvent.OnGameOver?.Invoke();
        }

        public void Update()
        {

        }

        public void Exit()
        {
            Debug.Log("Exit GameOver State");
        }
    }

}