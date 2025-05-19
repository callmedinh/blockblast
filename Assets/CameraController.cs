using System;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public void SetPosition(Vector2Int mapSize)
    {
        mainCamera.transform.position = new Vector3((float)mapSize.x / 2, (float)mapSize.y / 2, -10f);
    }
}
