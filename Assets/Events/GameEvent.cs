using System;
using UnityEngine.Tilemaps;

namespace Events
{
    public class GameEvent
    {
        public static Action OnGameOver;
        public static Action OnLinesCleared;
        public static Action OnBlockPlaced;
        public static Action OnGameStarted;
        public static Action<Tilemap> OnMapInitialized;
        public static Action<int> OnBestScoreChanged;
    }
}