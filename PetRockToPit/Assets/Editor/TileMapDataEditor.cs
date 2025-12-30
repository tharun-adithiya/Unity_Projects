using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(NewTilemapData))]
public class TileMapDataEditor : Editor
{
    
    public Tilemap platformTilemap;
    public Tilemap obstacleTilemap;
    public Tilemap levelEndTilemap;
    public Tilemap moveableObjectTilemap;
    public Tilemap buttonTilemap;



    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Tilemap export tools", EditorStyles.boldLabel);

        platformTilemap = (Tilemap)EditorGUILayout.ObjectField("PlatForm Tilemap", platformTilemap, typeof(Tilemap), true);
        obstacleTilemap = (Tilemap)EditorGUILayout.ObjectField("Obstacle Tilemap", obstacleTilemap, typeof(Tilemap), true);
        levelEndTilemap = (Tilemap)EditorGUILayout.ObjectField("Level End Tilemap", levelEndTilemap, typeof(Tilemap), true);
        moveableObjectTilemap = (Tilemap)EditorGUILayout.ObjectField("Moveable Object Tilemap", moveableObjectTilemap, typeof(Tilemap), true);
        buttonTilemap = (Tilemap)EditorGUILayout.ObjectField("Button Tilemap", buttonTilemap, typeof(Tilemap), true);


        if (GUILayout.Button("Export scene TileMaps to SO"))
        {
            ExportTiles((NewTilemapData)target);
        }

        
    }

    private void ExportTiles(NewTilemapData data)
    {
        data.platformTiles.Clear();
        data.obstacleTiles.Clear();
        data.levelEndTiles.Clear();
        data.moveableObjectTiles.Clear();
        data.buttonTiles.Clear();
        

        
        if(platformTilemap)ExportTilemap(platformTilemap, data.platformTiles, data.tilePalette);

        if(obstacleTilemap)ExportTilemap(obstacleTilemap, data.obstacleTiles, data.tilePalette);

        if(levelEndTilemap)ExportTilemap(levelEndTilemap, data.levelEndTiles, data.tilePalette);

        if(moveableObjectTilemap)ExportTilemap(moveableObjectTilemap, data.moveableObjectTiles, data.tilePalette);

        if(buttonTilemap) ExportTilemap(buttonTilemap, data.buttonTiles, data.tilePalette);
        
        EditorUtility.SetDirty(data);
        Debug.Log("Exported Tilemap data to SO");
    }

    private void ExportTilemap(Tilemap tilemap, List<NewTilemapData.TileData> tilesList, TileBase[] tilePalette)
    {
        if (tilemap == null || tilePalette == null) return;
        BoundsInt bounds = tilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                int typeID = System.Array.IndexOf(tilePalette, tile);
                tilesList.Add(new NewTilemapData.TileData { position = pos, tileTypeID = typeID });
            }
        }
    }
}
