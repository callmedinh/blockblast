using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "MapInfo", menuName = "Blockblast/MapInfo")]
    public class MapInfo : ScriptableObject
    {
        public Vector2Int mapSize;
        public Sprite backGround;
    }
}