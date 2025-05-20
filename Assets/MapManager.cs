using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    private int _width;
    private int _height;
    private Point[,] _points;
    
    public Tilemap blockTilemap;
    public Tilemap backgroundTileMap;
    public Tilemap ghostBlockTileMap;
    [SerializeField] private TileBase backgroundTileBase;
    [SerializeField] private TileBase tileBase;
    [SerializeField] private TileBase ghostBlockTileBase;

    public static Action InitNewBlocksAction;
    public void InitMap(MapInfo mapInfo)
    {
        _width = mapInfo.mapSize.x;
        _height = mapInfo.mapSize.y;
        _points = new Point[_width, _height];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _points[i, j] = new Point(i, j);
                backgroundTileMap.SetTile(new Vector3Int(i, j, 0), backgroundTileBase);
            }
        }
    }

    public void ShowGhostBlocks(BlockDragHandler block)
    {
        ghostBlockTileMap.ClearAllTiles();
        Transform[] cells = block.GetComponentsInChildren<Transform>();
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        
        foreach (var cell in cells)
        {
            Vector3Int tilPos = blockTilemap.WorldToCell(cell.position);
            if (!IsInsideMap(tilPos.x, tilPos.y)) return;
            if (ghostBlockTileMap.HasTile(tilPos))
            {
                return;
            }
            tilePositions.Add(tilPos);
        }

        foreach (var pos in tilePositions)
        {
            ghostBlockTileMap.SetTile(pos, ghostBlockTileBase);
        }
    }
    public void TryPlaceBlock(BlockDragHandler block)
    {
        Transform[] cells = block.GetComponentsInChildren<Transform>();
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        foreach (Transform cell in cells)
        {
            Vector3Int tilPos = blockTilemap.WorldToCell(cell.position);
            if (!IsInsideMap(tilPos.x, tilPos.y)) return;
            if (blockTilemap.HasTile(tilPos))
            {
                Debug.Log("Invalid placement: tile already filled");
                return;
            }
            tilePositions.Add(tilPos);
        }

        foreach (var pos in tilePositions)
        {
            blockTilemap.SetTile(pos, tileBase);
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
        return x >= 0 && y >= 0 && x < _width && y < _height;
    }

    public bool IsRowFull(int y)
    {
        for (int x = 0; x < _width; x++)
        {
            if (!blockTilemap.HasTile(new Vector3Int(x, y, 0)))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsColumnFull(int x)
    {
        for (int y = 0; y < _height; y++)
        {
            if (!blockTilemap.HasTile(new Vector3Int(x, y, 0)))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearRow(int y)
    {
        for (int x = 0;x < _width; x++)
        {
            blockTilemap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }

    public void ClearColumn(int x)
    {
        for (int y = 0; y < _height; y++)
        {
            blockTilemap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }

    public void ResetMap()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                blockTilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }
}
