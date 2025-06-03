using DefaultNamespace;
using Events;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : Singleton<CameraController>
{
    public Camera mainCamera;
    [Range(0, 20)]
    [SerializeField] private int offset = 2;

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        GameEvent.OnMapInitialized += FitCamera;
    }

    private void FitCamera(Tilemap tilemap)
    {
        Bounds bounds = tilemap.localBounds;

        float mapWidth = bounds.size.x;
        float mapHeight = bounds.size.y;
        
        float screenAspect = (float)Screen.width / Screen.height;
        
        float cameraSizeX = (mapWidth / 2f) / screenAspect;
        float cameraSizeY = mapHeight / 2f;

        mainCamera.orthographic = true;
        mainCamera.orthographicSize = Mathf.Max(cameraSizeX + offset, cameraSizeY + offset);
        
        Vector3 center = bounds.center;
        mainCamera.transform.position = new Vector3(center.x, center.y - 1f, -10f);
    }

    
}
