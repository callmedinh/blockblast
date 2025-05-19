using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    public int width;
    public int height;
    private Point[,] _points;
    
    public Tilemap tileMap;
    public Tilemap backgroundTileMap;
    public TileBase backgroundTileBase;
    [SerializeField] private TileBase tileBase;
    public Grid grid;

    public static Action InitNewBlocksAction;
    public void InitMap(MapInfo mapInfo)
    {
        width = mapInfo.mapSize.x;
        height = mapInfo.mapSize.y;
        _points = new Point[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _points[i, j] = new Point(i, j);
                backgroundTileMap.SetTile(new Vector3Int(i, j, 0), backgroundTileBase);
            }
        }
    }

    public void TryPlaceBlock(BlockDragHandler block)
    {
        Transform[] cells = block.GetComponentsInChildren<Transform>();
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        foreach (Transform cell in cells)
        {
            Vector3Int tilPos = tileMap.WorldToCell(cell.position);
            if (!IsInsideMap(tilPos.x, tilPos.y)) return;
            if (tileMap.HasTile(tilPos))
            {
                Debug.Log("Invalid placement: tile already filled");
                return;
            }
            tilePositions.Add(tilPos);
        }

        foreach (var pos in tilePositions)
        {
            tileMap.SetTile(pos, tileBase);
        }
        CheckAndClearLines(tilePositions);
        block.GetComponentInParent<Pooler>().ReturnToPool(block.gameObject);
        BlockManager.BlockCount--;
    }

    void CheckAndClearLines(List<Vector3Int> blockPos)
    {
        foreach (var pos in blockPos)
        {
            if (IsRowFull(pos.y))
            {
                ClearRow(pos.y);
            }

            if (IsColumnFull(pos.x))
            {
                ClearColumn(pos.x);
            }
        }
    }
    public bool IsInsideMap(float x, float y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (!tileMap.HasTile(new Vector3Int(x, y, 0)))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsColumnFull(int x)
    {
        for (int y = 0; y < height; y++)
        {
            if (!tileMap.HasTile(new Vector3Int(x, y, 0)))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearRow(int y)
    {
        for (int x = 0;x < width; x++)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }

    public void ClearColumn(int x)
    {
        for (int y = 0; y < height; y++)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }

    public void ResetMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tileMap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }
}
