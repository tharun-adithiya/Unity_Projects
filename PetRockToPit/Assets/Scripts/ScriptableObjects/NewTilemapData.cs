using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewTilemapData", menuName = "Scriptable Objects/NewTilemapData")]
public class NewTilemapData : ScriptableObject
{
    public List<TileData> platformTiles = new List<TileData>();
    public List<TileData> levelEndTiles = new List<TileData>();
    public List<TileData> obstacleTiles = new List<TileData>();
    public List<TileData> moveableObjectTiles = new List<TileData>();
    public List<TileData> buttonTiles = new List<TileData>();
    
    public TileBase[] tilePalette;

    [System.Serializable]
    public struct TileData
    {
        public Vector3Int position;
        public int tileTypeID; // Map this ID to an actual TileBase
    }
}
