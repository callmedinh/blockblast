using Audio;
using Block;
using DefaultNamespace;
using Events;
using Map;
using UI;

namespace Core
{
    public class GameplayState : IGameState
    {
        private readonly MapInfo _mapInfo;
        private readonly BlockSystem _blockSystem;

        public GameplayState(MapInfo mapInfo, BlockSystem blockSystem)
        {
            _mapInfo = mapInfo;
            _blockSystem = blockSystem;
        }

        public void Enter()
        {
            GameEvent.OnGameStarted?.Invoke();
            if (_blockSystem.availableBlocks.Count == 0)
            {
                _blockSystem.SpawnRandomBlocks(3);
            }
            MapManager.Instance.InitMap(_mapInfo);
            UIManager.Instance.ShowScreen(ScreenType.Gameplay);
            SoundManager.Instance.Play(State.Gameplay);
            GameEvent.OnBlockPlaced += HandleBlockPlaced;
        }

        public void Update()
        {
            if (_blockSystem.HasAvailableBlocks && !MapManager.Instance.CanPlaceAnyBlocks(_blockSystem.availableBlocks))
            {
                GameManager.Instance.ChangeState(GameState.GameOver);
            }else if (!_blockSystem.HasAvailableBlocks)
            {
                _blockSystem.SpawnRandomBlocks(3);
            }
            
        }

        void HandleBlockPlaced(BlockController block)
        {
            _blockSystem.RemoveBlock(block);
        }
        public void Exit()
        {
            
        }
    }

}