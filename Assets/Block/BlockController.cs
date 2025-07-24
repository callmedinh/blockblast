using UnityEngine;

namespace Block
{
    public class BlockController : MonoBehaviour
    {
        private SpriteRenderer[] _spriteRenderer;
        [SerializeField] private Sprite sprite;
        public Vector2Int[] cellsInfo;

        private void Awake()
        {
            _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRender in _spriteRenderer)
            {
                spriteRender.sprite = sprite;
            }
        }
    }
}
