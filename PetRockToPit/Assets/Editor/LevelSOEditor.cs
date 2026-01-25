using log4net.Core;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using static LevelSO;

[CustomEditor(typeof(LevelSO))]
public class LevelSOEditor : Editor
{
    [SerializeField]public bool showExportPreview = true;

    // ================= Scene References =================

    GameObject grid;
    NewTilemapData tilemapData;

    GameObject playerObj;
    CinemachineCamera cam;

    Transform canonBallBoundsParent;

    GameObject canonObject1;
    Transform canon1WaypointParent;


    GameObject canonObject2;
    Transform canon2WaypointParent;

    GameObject canonObject3;
    Transform canon3WaypointParent;

    GameObject canonObject4;
    Transform canon4WaypointParent;

    GameObject moveablePlatformTile;
    Transform waypointA;
    Transform waypointB;

    GameObject teleportEntry;
    GameObject teleportExit;

    //Properties for limited moves level
    [SerializeField]int numberOfMoves;

    //Properties for Limited Shots Level
    [SerializeField] int canon1Shots;
    [SerializeField] int canon2Shots;
    [SerializeField] int canon3Shots;
    [SerializeField] int canon4Shots;

    //Property for timed level
    [SerializeField] float time;                  

    //Property for bomb level
    GameObject bomb;

    //Properties for ColorMatching level
    
    LevelSO.CanonType canon1Type = LevelSO.CanonType.Default;
    LevelSO.CanonType canon2Type = LevelSO.CanonType.Default;
    LevelSO.CanonType canon3Type = LevelSO.CanonType.Default;
    LevelSO.CanonType canon4Type = LevelSO.CanonType.Default;

    // ================= Serialized =================

    SerializedProperty levelTypeProp;
    SerializedProperty levelDataProp;
    

    private void OnEnable()
    {
        levelTypeProp = serializedObject.FindProperty("levelType");
        levelDataProp = serializedObject.FindProperty("levelData");
        
    }

    public override void OnInspectorGUI()
    {
        LevelSO levelSO = (LevelSO)target;
        EnsureLevelDataExists(levelSO);
        serializedObject.Update();

        // ===== Level Type =====
        EditorGUILayout.PropertyField(levelTypeProp);
        EnsureCorrectLevelDataInstance(levelSO);
        
        EditorGUILayout.Space();

        // ===== Level-type specific editor UI =====
        switch (levelSO.levelType)
        {
            case LevelSO.LevelType.MoveablePlatform:
                DrawMoveablePlatformSection();
                break;

            case LevelSO.LevelType.Portals:
                DrawTeleportSection();
                break;
            case LevelSO.LevelType.LimitedMoves:
                DrawLimitedMovesSection();
                break;
            case LevelSO.LevelType.Timed:
                DrawTimedLevelSection();
                break;
            case LevelSO.LevelType.LimitedShots:
                DrawLimitedShotsSection();
                break;
            case LevelSO.LevelType.Bomb:
                DrawBombLevelSection();
                break;
            case LevelSO.LevelType.ColorMatching:
                DrawColorMatchSection();
                break;

        }

        EditorGUILayout.Space();
        DrawExportedDataPreview(levelSO);
        DrawCommonExportSection();

        serializedObject.ApplyModifiedProperties();
    }

    // ================= UI SECTIONS =================

    void DrawMoveablePlatformSection()
    {
        EditorGUILayout.LabelField("Moveable Platform (Scene Refs)", EditorStyles.boldLabel);

        moveablePlatformTile =
            (GameObject)EditorGUILayout.ObjectField(
                "Platform Tile",
                moveablePlatformTile,
                typeof(GameObject),
                true
            );

        waypointA =
            (Transform)EditorGUILayout.ObjectField(
                "Waypoint A",
                waypointA,
                typeof(Transform),
                true
            );

        waypointB =
            (Transform)EditorGUILayout.ObjectField(
                "Waypoint B",
                waypointB,
                typeof(Transform),
                true
            );
    }

    void DrawTeleportSection()
    {
        EditorGUILayout.LabelField("Teleport (Scene Refs)", EditorStyles.boldLabel);

        teleportEntry =
            (GameObject)EditorGUILayout.ObjectField(
                "Teleport Entry",
                teleportEntry,
                typeof(GameObject),
                true
            );

        teleportExit =
            (GameObject)EditorGUILayout.ObjectField(
                "Teleport Exit",
                teleportExit,
                typeof(GameObject),
                true
            );
    }
    void DrawLimitedMovesSection()
    {
        EditorGUILayout.LabelField("LimitedMoves", EditorStyles.boldLabel);
        numberOfMoves = EditorGUILayout.IntField("Number of moves", Mathf.Clamp(numberOfMoves,0,100));
    }

    void DrawBombLevelSection()
    {
        EditorGUILayout.LabelField("Bomb Level", EditorStyles.boldLabel);
        bomb = (GameObject)EditorGUILayout.ObjectField("Bomb Prefab", bomb, typeof(GameObject), true);
    }
    void DrawTimedLevelSection()
    {
        EditorGUILayout.LabelField("Timed Level", EditorStyles.boldLabel);
        time = EditorGUILayout.FloatField("Time",time);
    }
    void DrawLimitedShotsSection()
    {
        EditorGUILayout.LabelField("Limited Shots Level", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        canon1Shots = EditorGUILayout.IntField("Shots1", canon1Shots);
        canon2Shots = EditorGUILayout.IntField("Shots2", canon2Shots);
        canon3Shots = EditorGUILayout.IntField("Shots3", canon3Shots);
        canon4Shots = EditorGUILayout.IntField("Shots4", canon4Shots);
    }
    void DrawColorMatchSection()
    {
        EditorGUILayout.LabelField("Color Match Level", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        canon1Type = (LevelSO.CanonType)EditorGUILayout.EnumPopup("Canon1 Type", canon1Type);
        canon2Type = (LevelSO.CanonType)EditorGUILayout.EnumPopup("Canon2 Type", canon2Type);
        canon3Type = (LevelSO.CanonType)EditorGUILayout.EnumPopup("Canon3 Type", canon3Type);
        canon4Type = (LevelSO.CanonType)EditorGUILayout.EnumPopup("Canon4 Type", canon4Type);
    }
    void DrawCommonExportSection()
    {
        EditorGUILayout.LabelField("Level Data Export Tools", EditorStyles.boldLabel);

        grid =
            (GameObject)EditorGUILayout.ObjectField(
                "Grid",
                grid,
                typeof(GameObject),
                true
            );
        tilemapData =
            (NewTilemapData)EditorGUILayout.ObjectField(
                "Tilemap Data",
                tilemapData,
                typeof(NewTilemapData),
                true
            );
                
        EditorGUILayout.Space();

        playerObj =
            (GameObject)EditorGUILayout.ObjectField(
                "Player",
                playerObj,
                typeof(GameObject),
                true
            );

        EditorGUILayout.Space();

        canonBallBoundsParent =
            (Transform)EditorGUILayout.ObjectField(
                "Canon Ball Bounds",
                canonBallBoundsParent,
                typeof(Transform),
                true
            );

        DrawCanonSection("Canon 1", ref canonObject1, ref canon1WaypointParent);
        DrawCanonSection("Canon 2", ref canonObject2, ref canon2WaypointParent);
        DrawCanonSection("Canon 3", ref canonObject3, ref canon3WaypointParent);
        DrawCanonSection("Canon 4", ref canonObject4, ref canon4WaypointParent);

        EditorGUILayout.Space();

        cam =
            (CinemachineCamera)EditorGUILayout.ObjectField(
                "Camera",
                cam,
                typeof(CinemachineCamera),
                true
            );

        EditorGUILayout.Space();

        if (GUILayout.Button("Export Positions & Waypoints to LevelSO"))
        {
            ExportData();
            
        }
    }
    void DrawCanonSection(
        string label,
        ref GameObject canon,
        ref Transform waypointParent)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);

        canon =
            (GameObject)EditorGUILayout.ObjectField(
                "Canon",
                canon,
                typeof(GameObject),
                true
            );

        waypointParent =
            (Transform)EditorGUILayout.ObjectField(
                "Waypoint Parent",
                waypointParent,
                typeof(Transform),
                true
            );
    }

    // ================= EXPORT =================

    private void ExportData()
    {
        LevelSO levelSO = (LevelSO)target;
        LevelSO.Default data = levelSO.levelData;
        
        // ==== TILEMAP ===
        if(tilemapData)data.tilemapData = tilemapData;
        // ===== GRID =====
        if (grid)
            data.gridPosition = grid.transform.position;

        // ===== PLAYER =====
        if (playerObj)
            data.playerPosition = playerObj.transform.position;

        // ===== CAMERA =====
        if (cam)
        {
            data.cameraOrthographicSize = cam.Lens.OrthographicSize;
            data.cameraPosition = cam.transform.position;
        }
         
        // ===== CANON BALL BOUNDS =====
        if (canonBallBoundsParent)
        {
            data.canonBallBoundsParentPosition =
                canonBallBoundsParent.position;
            data.canonBallBoundsPositions =
                ExtractWaypoints(canonBallBoundsParent);
        }

        // ===== CANONS =====
        ExportCanon(canonObject1, canon1WaypointParent,
            ref data.canonObject1Pos,
            ref data.canonObject1Waypoints,
            ref data.canon1WaypointsParentPosition,
            ref data.canon1WaypointsHolderScale);

        ExportCanon(canonObject2, canon2WaypointParent,
            ref data.canonObject2Pos,
            ref data.canonObject2Waypoints,
            ref data.canon2WaypointsParentPosition,
            ref data.canon2WaypointsHolderScale);

        ExportCanon(canonObject3, canon3WaypointParent,
            ref data.canonObject3Pos,
            ref data.canonObject3Waypoints,
            ref data.canon3WaypointsParentPosition,
            ref data.canon3WaypointsHolderScale);

        ExportCanon(canonObject4, canon4WaypointParent,
            ref data.canonObject4Pos,
            ref data.canonObject4Waypoints,
            ref data.canon4WaypointsParentPosition,
            ref data.canon4WaypointsHolderScale);

        // ===== Switch based on the current level type =====
        /*switch (levelSO.levelData)
        {
            case LevelSO.MoveablePlatform mp:
                if (moveablePlatformTile) mp.moveablePlatformPosition = moveablePlatformTile.transform.position;
                if (waypointA) mp.waypointAPosition = waypointA.position;
                if (waypointB) mp.waypointBPosition = waypointB.position;
                break;
            case LevelSO.Teleport tp:
                if (teleportEntry) tp.teleportEntryPosition = teleportEntry.transform.position;
                if (teleportExit) tp.teleportExitPosition = teleportExit.transform.position;
                break;
            case LevelSO.BombLevel bombLevel:
                bombLevel.bombPosition = bomb.transform.position;
                break;
            case LevelSO.LimitedMoves lm:
                lm.numberOfMoves = numberOfMoves;
                break;
            case LevelSO.LimitedShots limitedShotsLevel:
                limitedShotsLevel.canon1NumberOfShots = canon1Shots;
                limitedShotsLevel.canon2NumberOfShots = canon2Shots;
                limitedShotsLevel.canon3NumberOfShots = canon3Shots;
                limitedShotsLevel.canon4NumberOfShots = canon4Shots;
                break;
            case LevelSO.Timed timedLevel:
                timedLevel.time= time;
                break;
        }*/
        if (data is LevelSO.MoveablePlatform mp)
        {
            if (moveablePlatformTile)
                mp.moveablePlatformPosition =
                    moveablePlatformTile.transform.position;

            if (waypointA)
                mp.waypointAPosition = waypointA.position;

            if (waypointB)
                mp.waypointBPosition = waypointB.position;
        }
        // ===== TELEPORT =====
        if (data is LevelSO.Teleport tp)
        {
            if (teleportEntry)
                tp.teleportEntryPosition = teleportEntry.transform.position;

            if (teleportExit)
                tp.teleportExitPosition = teleportExit.transform.position;
        }
        if (data is LevelSO.BombLevel bombLevel)
        {
            bombLevel.bombPosition = bomb.transform.position;
        }
        if (data is LevelSO.LimitedMoves lm)
        {
            lm.numberOfMoves = numberOfMoves;
        }
        if (data is LevelSO.Timed timedLevelData)
        {
            timedLevelData.time = time;
        }
        if (data is LevelSO.LimitedShots limitedShotsLevel)
        {
            limitedShotsLevel.canon1NumberOfShots = canon1Shots;
            limitedShotsLevel.canon2NumberOfShots = canon2Shots;
            limitedShotsLevel.canon3NumberOfShots = canon3Shots;
            limitedShotsLevel.canon4NumberOfShots = canon4Shots;
        }
        if (data is LevelSO.ColourMatch colorMatchLevel)
        {
            colorMatchLevel.canonTypes[0] = canon1Type;
            colorMatchLevel.canonTypes[1] = canon2Type;
            colorMatchLevel.canonTypes[2] = canon3Type;
            colorMatchLevel.canonTypes[3] = canon4Type;
        }
        EditorUtility.SetDirty(levelSO);
        AssetDatabase.SaveAssets();
        Debug.Log("LevelSO export complete.");
    }
    private void DrawExportedDataPreview(LevelSO levelSO)
    {
        if (levelSO.levelData == null) return;

        showExportPreview = EditorGUILayout.Foldout(
            showExportPreview,
            "Exported Data Preview (Read Only)",
            true
        );

        if (!showExportPreview) return;

        EditorGUI.BeginDisabledGroup(true); 
        EditorGUI.indentLevel++;

        LevelSO.Default data = levelSO.levelData;

        // ===== GRID =====
        EditorGUILayout.LabelField("Grid", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Grid Position", data.gridPosition);

        //===== TILEMAPDATA ======
        EditorGUILayout.LabelField("Tilemap data", EditorStyles.boldLabel);
            data.tilemapData = (NewTilemapData)EditorGUILayout.ObjectField(
            "Tilemap Data",
            data.tilemapData,
            typeof(NewTilemapData),
            false
        );

        // ===== PLAYER & CAMERA =====
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Player & Camera", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Player Position", data.playerPosition);
        EditorGUILayout.FloatField("Camera Ortho Size", data.cameraOrthographicSize);

        // ===== CANON BALL BOUNDS =====
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Canon Ball Bounds", EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field(
            "Bounds Parent Position",
            data.canonBallBoundsParentPosition
        );

        DrawVectorList("Bounds Waypoints", data.canonBallBoundsPositions);

        // ===== CANONS =====
        DrawCanonPreview("Canon 1", data.canonObject1Pos, data.canonObject1Waypoints,data.canon1WaypointsParentPosition,data.canon1WaypointsHolderScale);
        DrawCanonPreview("Canon 2", data.canonObject2Pos, data.canonObject2Waypoints, data.canon2WaypointsParentPosition,data.canon2WaypointsHolderScale);
        DrawCanonPreview("Canon 3", data.canonObject3Pos, data.canonObject3Waypoints, data.canon3WaypointsParentPosition,data.canon3WaypointsHolderScale);
        DrawCanonPreview("Canon 4", data.canonObject4Pos, data.canonObject4Waypoints, data.canon4WaypointsParentPosition,data.canon4WaypointsHolderScale);

        // ===== MOVEABLE PLATFORM =====
        if (data is LevelSO.MoveablePlatform mp)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Moveable Platform", EditorStyles.boldLabel);
            EditorGUILayout.Vector3Field("Platform Position", mp.moveablePlatformPosition);
            EditorGUILayout.Vector3Field("Waypoint A", mp.waypointAPosition);
            EditorGUILayout.Vector3Field("Waypoint B", mp.waypointBPosition);
        }

        // ===== TELEPORT =====
        if (data is LevelSO.Teleport tp)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Teleport", EditorStyles.boldLabel);
            EditorGUILayout.Vector3Field("Entry Position", tp.teleportEntryPosition);
            EditorGUILayout.Vector3Field("Exit Position", tp.teleportExitPosition);
        }
        if (data is LevelSO.LimitedMoves lm)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Limited Moves", EditorStyles.boldLabel);
            EditorGUILayout.IntField("Number of moves", lm.numberOfMoves);
        }
        if (data is LevelSO.LimitedShots limitedShotsLevel)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Limited Shots", EditorStyles.boldLabel);
            EditorGUILayout.IntField("Number of moves", limitedShotsLevel.canon1NumberOfShots);
            EditorGUILayout.IntField("Number of moves", limitedShotsLevel.canon2NumberOfShots);
            EditorGUILayout.IntField("Number of moves", limitedShotsLevel.canon3NumberOfShots);
            EditorGUILayout.IntField("Number of moves", limitedShotsLevel.canon4NumberOfShots);
        }
        if (data is LevelSO.Timed timedLevelData)
        {
            EditorGUILayout.Space();
            EditorGUILayout.FloatField("Time", timedLevelData.time);
        }
        if (data is LevelSO.BombLevel bombLevel)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Bomb level", EditorStyles.boldLabel);
            EditorGUILayout.Vector3Field("Bomb Position", bombLevel.bombPosition);
        }
        if (data is LevelSO.ColourMatch colorMatchLevelData)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color Match Level", EditorStyles.boldLabel);
            EditorGUILayout.EnumFlagsField("Canon1 Type", colorMatchLevelData.canonTypes[0]);
            EditorGUILayout.EnumFlagsField("Canon2 Type", colorMatchLevelData.canonTypes[1]);
            EditorGUILayout.EnumFlagsField("Canon3 Type", colorMatchLevelData.canonTypes[2]);
            EditorGUILayout.EnumFlagsField("Canon4 Type", colorMatchLevelData.canonTypes[3]);
        }
        EditorGUI.indentLevel--;
        EditorGUI.EndDisabledGroup();
    }

    private void ExportCanon(
    GameObject canon,
    Transform waypointParent,
    ref Vector3 canonPos,
    ref List<Vector3> waypoints,
    ref Vector3 holderPos,
    ref Vector3 holderScale)
    {
        if (canon)
            canonPos = canon.transform.position;

        if (waypointParent)
        {
            waypoints = ExtractWaypoints(waypointParent);
            holderPos = waypointParent.position;
            holderScale = waypointParent.localScale;
        }
    }

    private void DrawCanonPreview(
    string label,
    Vector3 canonPos,
    List<Vector3> waypoints,Vector3 canonSliderPos,Vector3 sliderScale)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        EditorGUILayout.Vector3Field("Position", canonPos);
        EditorGUILayout.Vector3Field("Slider position", canonSliderPos);
        EditorGUILayout.Vector3Field("Slider scale", sliderScale);
        DrawVectorList("Waypoints", waypoints);
    }

    private void DrawVectorList(string label, List<Vector3> list)
    {
        if (list == null || list.Count == 0)
        {
            EditorGUILayout.LabelField(label, "None");
            return;
        }

        EditorGUILayout.LabelField(label);

        EditorGUI.indentLevel++;
        for (int i = 0; i < list.Count; i++)
        {
            EditorGUILayout.Vector3Field($"[{i}]", list[i]);
        }
        EditorGUI.indentLevel--;
    }

    private List<Vector3> ExtractWaypoints(Transform parent)
    {
        List<Vector3> list = new();

        if (!parent) return list;

        foreach (Transform child in parent)
            list.Add(child.localPosition);

        return list;
    }

    // ================= LEVEL TYPE HANDLING =================

    private void EnsureCorrectLevelDataInstance(LevelSO levelSO)
    {
        if (levelSO.levelData == null || !IsCorrectType(levelSO))
        {
            switch (levelSO.levelType)
            {
                case LevelSO.LevelType.Default:
                    levelSO.levelData = new LevelSO.Default();
                    break;

                case LevelSO.LevelType.MoveablePlatform:
                    levelSO.levelData = new LevelSO.MoveablePlatform();
                    break;

                case LevelSO.LevelType.Portals:
                    levelSO.levelData = new LevelSO.Teleport();
                    break;
                case LevelSO.LevelType.LimitedMoves:
                    levelSO.levelData=new LevelSO.LimitedMoves();
                    break;
                case LevelSO.LevelType.LimitedShots:
                    levelSO.levelData = new LevelSO.LimitedShots();
                    break;
                case LevelSO.LevelType.Timed:
                    levelSO.levelData=new LevelSO.Timed();
                    break;
                case LevelSO.LevelType.Bomb:
                    levelSO.levelData = new LevelSO.BombLevel();
                    break;
                case LevelSO.LevelType.ColorMatching:
                    levelSO.levelData = new LevelSO.ColourMatch();
                    break;
            }
        }
    }
    private void EnsureLevelDataExists(LevelSO levelSO)
    {
        if (levelSO.levelData != null)
        {
            return;
        }
        else Debug.LogWarning("LevelData is not properly assigned");

        switch (levelSO.levelType)
        {
            case LevelSO.LevelType.MoveablePlatform:
                levelSO.levelData = new LevelSO.MoveablePlatform();
                break;

            case LevelSO.LevelType.Portals:
                levelSO.levelData = new LevelSO.Teleport();
                break;

            case LevelSO.LevelType.LimitedMoves:
                levelSO.levelData=new LevelSO.LimitedMoves();
                break;
            case LevelSO.LevelType.LimitedShots:
                levelSO.levelData = new LevelSO.LimitedShots();
                break;
            case LevelSO.LevelType.Timed:
                levelSO.levelData=new LevelSO.Timed();
                break;
            case LevelSO.LevelType.Bomb:
                levelSO.levelData = new LevelSO.BombLevel();
                break;
            case LevelSO.LevelType.ColorMatching:
                levelSO.levelData = new LevelSO.ColourMatch();
                break;

            default:
                levelSO.levelData = new LevelSO.Default();
                break;
        }

        EditorUtility.SetDirty(levelSO);
    }

    private bool IsCorrectType(LevelSO levelSO)
    {
        return levelSO.levelType switch
        {
            LevelSO.LevelType.Default =>
                levelSO.levelData.GetType() == typeof(LevelSO.Default),

            LevelSO.LevelType.MoveablePlatform =>
                levelSO.levelData is LevelSO.MoveablePlatform,

            LevelSO.LevelType.Portals =>
                levelSO.levelData is LevelSO.Teleport,

            LevelSO.LevelType.LimitedMoves =>
                levelSO.levelData is LevelSO.LimitedMoves,

            LevelSO.LevelType.LimitedShots =>
                levelSO.levelData is LevelSO.LimitedShots,

            LevelSO.LevelType.Timed=>
                levelSO.levelData is LevelSO.Timed,

            LevelSO.LevelType.Bomb =>
                levelSO.levelData is LevelSO.BombLevel,
            LevelSO.LevelType.ColorMatching =>
                levelSO.levelData is LevelSO.ColourMatch,
            _ => false
        };
    }
}
