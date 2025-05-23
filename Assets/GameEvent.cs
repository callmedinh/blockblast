using System;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public static class GameEvent
    {
        public static Action OnGameOver;
        public static Action OnLinesCleared;
        public static Action OnBlockPlaced;
        public static Action OnGameStarted;
        public static Action<Tilemap> OnMapInitialized;
    }
}