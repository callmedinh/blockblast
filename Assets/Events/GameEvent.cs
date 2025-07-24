using System;
using Block;
using UnityEngine.Tilemaps;

namespace Events
{
    public class GameEvent
    {
        public static Action OnGameOver;
        public static Action OnLinesCleared;
        public static Action<BlockController> OnBlockPlaced;
        public static Action OnGameStarted;
        public static Action<Tilemap> OnMapInitialized;
        public static Action<int> OnBestScoreChanged;
        
        //State Games
        public static Action OnPause;
        public static Action OnResume;
    }
}