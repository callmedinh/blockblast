using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BlockDragHandler : MonoBehaviour
{
    private Transform selectedObject;
    public bool isDragging = false;
    private Vector2 mousePosition;
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (isDragging)
        {
            transform.position = mousePosition;
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
    }
}
