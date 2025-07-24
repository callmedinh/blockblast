using Block;
using UI;

namespace Core
{
    public class GamePauseState : IGameState
    {
        BlockSystem _blockSystem;
        public GamePauseState(BlockSystem blockSystem)
        {
            _blockSystem = blockSystem;
        }
        public void Enter()
        {
            UIManager.Instance.ShowScreen(ScreenType.Setting);
            _blockSystem.HideActiveBlocks();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            _blockSystem.ShowActiveBlocks();
        }
    }
}