using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Blockblast/Block")]
public class Block : MonoBehaviour
{
    [SerializeField] private Color color;
    private SpriteRenderer[] _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in _spriteRenderer)
        {
            sprite.color = color;
        }
    }
}
