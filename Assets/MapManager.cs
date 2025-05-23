
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : Singleton<MapManager>
{
    private int _width;
    private int _height;
    public Tilemap blockTilemap;
    public Tilemap backgroundTilemap;
    public Tilemap ghostBlockTilemap;
    [SerializeField] private TileBase backgroundTileBase;
    [SerializeField] private TileBase blockTileBase;
    [SerializeField] private TileBase ghostBlockTileBase;
    [SerializeField] private Sound clearSound;
    [SerializeField] private Sound dropSound;

    private void OnEnable()
    {
        GameEvent.OnGameOver += ResetMap;
    }

    private void OnDisable()
    {
        GameEvent.OnGameOver -= ResetMap;
    }

    #region Map Initialization
    public void InitMap(MapInfo mapInfo)
    {
        _width = mapInfo.mapSize.x;
        _height = mapInfo.mapSize.y;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                backgroundTilemap.SetTile(new Vector3Int(i, j, 0), backgroundTileBase);
            }
        }

        GameEvent.OnMapInitialized?.Invoke(backgroundTilemap);
    }   
    #endregion


    #region Block Placement

        public bool CanPlaceAnyBlocks(List<Block> availableBlocks)
    {
        List<Vector2Int> freeTile = new();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (!blockTilemap.HasTile(pos))
                {
                    freeTile.Add(new Vector2Int(x, y));
                }
            }
        }

        foreach (var block in availableBlocks)
        {
            Vector2Int[] cells = block.cellsInfo;
            foreach (var origin in freeTile)
            {
                bool canPlace = true;
                foreach (var offset in cells)
                {
                    Vector2Int pos = offset + origin;
                    if (!IsInsideMap(pos.x, pos.y) || blockTilemap.HasTile(new Vector3Int(pos.x, pos.y, 0)))
                    {
                        canPlace = false;
                    }
                }

                if (canPlace) return true;
            }
        }
        
        return false;
    }

    public void ShowGhostPreview(BlockDragHandler dragHandler)
    {
        ghostBlockTilemap.ClearAllTiles();
        Transform[] cells = dragHandler.transform.Cast<Transform>().ToArray();
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        
        foreach (var cell in cells)
        {
            Vector3Int tilPos = blockTilemap.WorldToCell(cell.position);
            if (!IsInsideMap(tilPos.x, tilPos.y)) return;
            if (ghostBlockTilemap.HasTile(tilPos))
            {
                return;
            }
            tilePositions.Add(tilPos);
        }

        foreach (var pos in tilePositions)
        {
            ghostBlockTilemap.SetTile(pos, ghostBlockTileBase);
        }
    }
    public void TryPlaceBlock(BlockDragHandler dragHandler)
    {
        Transform[] cells = dragHandler.transform.Cast<Transform>().ToArray();
        Block block = dragHandler.GetComponent<Block>();
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        foreach (Transform cell in cells)
        {
            Vector3Int tilPos = blockTilemap.WorldToCell(cell.position);
            if (!IsInsideMap(tilPos.x, tilPos.y)) return;
            if (blockTilemap.HasTile(tilPos))
            {
                return;
            }
            tilePositions.Add(tilPos);
        }

        foreach (var pos in tilePositions)
        {
            blockTilemap.SetTile(pos, blockTileBase);
        }
        GameEvent.OnBlockPlaced();
        SoundManager.Instance.PlaySFX(dropSound);
        CheckAndClearLines(tilePositions);
        BlockManager.Instance.RemoveBlock(block);
        dragHandler.GetComponentInParent<Pooler>().ReturnToPool(dragHandler.gameObject);
    }

    #endregion


    void CheckAndClearLines(List<Vector3Int> blockPos)
    {
        foreach (var pos in blockPos)
        {
            if (IsRowFull(pos.y))
            {
                ClearRow(pos.y);
                SoundManager.Instance.PlaySFX(clearSound);
                GameEvent.OnLinesCleared?.Invoke();
            }

            if (IsColumnFull(pos.x))
            {
                ClearColumn(pos.x);
                SoundManager.Instance.PlaySFX(clearSound);
                GameEvent.OnLinesCleared?.Invoke();
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
        blockTilemap.ClearAllTiles();
        ghostBlockTilemap.ClearAllTiles();
        backgroundTilemap.ClearAllTiles();
    }
}
