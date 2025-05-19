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
    
    /*
    void SelectedObject()
    {
        Collider2D hit = Physics2D.OverlapPoint(mousePosition);
        if (hit != null)
        {
            selectedObject = hit.transform;
            isDragging = true;
        }
    }

    void DragObject(Vector3 position)
    {
        selectedObject.position = position;
    }
    */
}
