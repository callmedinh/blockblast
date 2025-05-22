using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BlockDragHandler : MonoBehaviour
{
    [SerializeField] private Sound dragSound;
    private Transform _selectedObject;
    public bool isDragging = false;
    private Vector2 _mousePosition;

    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (isDragging)
        {
            transform.position = _mousePosition;
            MapManager.Instance.ShowGhostBlocks(this);
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
                MapManager.Instance.TryPlaceBlock(this);
            }
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        SoundManager.Instance.PlaySFX(dragSound);
    }
}
