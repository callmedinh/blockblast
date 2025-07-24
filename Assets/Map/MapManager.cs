using System.Collections.Generic;
using System.Linq;
using Audio;
using Block;
using DefaultNamespace;
using Events;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
    public class MapManager : Singleton<MapManager>
    {
        public Tilemap blockTilemap;
        public Tilemap backgroundTilemap;
        public Tilemap ghostBlockTilemap;
        [SerializeField] private TileBase backgroundTileBase;
        [SerializeField] private TileBase blockTileBase;
        [SerializeField] private TileBase ghostBlockTileBase;
        private int _height;
        private int _width;

        #region Map Initialization

        public void InitMap(MapInfo mapInfo)
        {
            _width = mapInfo.mapSize.x;
            _height = mapInfo.mapSize.y;
            for (var i = 0; i < _width; i++)
            for (var j = 0; j < _height; j++)
                backgroundTilemap.SetTile(new Vector3Int(i, j, 0), backgroundTileBase);

            GameEvent.OnMapInitialized?.Invoke(backgroundTilemap);
        }

        #endregion


        private void CheckAndClearLines(List<Vector3Int> blockPos)
        {
            foreach (var pos in blockPos)
            {
                if (IsRowFull(pos.y))
                {
                    ClearRow(pos.y);
                    SoundManager.Instance.Play(State.Satisfy);
                    GameEvent.OnLinesCleared?.Invoke();
                }

                if (IsColumnFull(pos.x))
                {
                    ClearColumn(pos.x);
                    SoundManager.Instance.Play(State.Satisfy);
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
            for (var x = 0; x < _width; x++)
                if (!blockTilemap.HasTile(new Vector3Int(x, y, 0)))
                    return false;

            return true;
        }

        private bool IsColumnFull(int x)
        {
            for (var y = 0; y < _height; y++)
                if (!blockTilemap.HasTile(new Vector3Int(x, y, 0)))
                    return false;

            return true;
        }

        private void ClearRow(int y)
        {
            for (var x = 0; x < _width; x++) blockTilemap.SetTile(new Vector3Int(x, y, 0), null);
        }

        private void ClearColumn(int x)
        {
            for (var y = 0; y < _height; y++) blockTilemap.SetTile(new Vector3Int(x, y, 0), null);
        }

        public void ResetMap()
        {
            blockTilemap.ClearAllTiles();
            ghostBlockTilemap.ClearAllTiles();
            backgroundTilemap.ClearAllTiles();
        }


        #region Block Placement

        public bool CanPlaceAnyBlocks(List<BlockController> availableBlocks)
        {
            List<Vector2Int> freeTile = new();
            for (var x = 0; x < _width; x++)
            for (var y = 0; y < _height; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                if (!blockTilemap.HasTile(pos)) freeTile.Add(new Vector2Int(x, y));
            }

            foreach (var block in availableBlocks)
            {
                var cells = block.cellsInfo;
                foreach (var origin in freeTile)
                {
                    var canPlace = true;
                    foreach (var offset in cells)
                    {
                        var pos = offset + origin;
                        if (!IsInsideMap(pos.x, pos.y) || blockTilemap.HasTile(new Vector3Int(pos.x, pos.y, 0)))
                            canPlace = false;
                    }

                    if (canPlace) return true;
                }
            }

            return false;
        }

        public void ShowGhostPreview(BlockDragHandler dragHandler)
        {
            ghostBlockTilemap.ClearAllTiles();
            var cells = dragHandler.transform.Cast<Transform>().ToArray();
            var tilePositions = new List<Vector3Int>();

            foreach (var cell in cells)
            {
                var tilPos = blockTilemap.WorldToCell(cell.position);
                if (!IsInsideMap(tilPos.x, tilPos.y)) return;
                if (ghostBlockTilemap.HasTile(tilPos)) return;
                tilePositions.Add(tilPos);
            }

            foreach (var pos in tilePositions) ghostBlockTilemap.SetTile(pos, ghostBlockTileBase);
        }

        public void TryPlaceBlock(BlockDragHandler dragHandler)
        {
            Transform[] cells = dragHandler.transform.GetComponentsInChildren<Transform>()
                .Where(t => t != dragHandler.transform) // nếu bạn muốn bỏ chính nó
                .ToArray();
            var block = dragHandler.GetComponent<BlockController>();
            var tilePositions = new List<Vector3Int>();
            foreach (var cell in cells)
            {
                var tilPos = blockTilemap.WorldToCell(cell.position);
                if (!IsInsideMap(tilPos.x, tilPos.y)) return;
                if (blockTilemap.HasTile(tilPos)) return;
                tilePositions.Add(tilPos);
            }
            foreach (var pos in tilePositions) blockTilemap.SetTile(pos, blockTileBase);
            SoundManager.Instance.Play(State.Drop);
            CheckAndClearLines(tilePositions);
            GameEvent.OnBlockPlaced?.Invoke(block);
        }

        #endregion
    }
}