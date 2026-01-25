using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    public enum LevelType
    {
        Default,
        MoveablePlatform,
        Portals,
        LimitedMoves,
        LimitedShots,       
        Timed,
        Bomb,
        ColorMatching
    }
    public enum CanonType
    {
        Default, 
        Blue, 
        Pink 
    };
    public LevelType levelType;

    [SerializeReference] public Default levelData;
    [SerializeReference] public LimitedMoves limitedMovesLevelData;

    // ================= BASE =================
    [System.Serializable]
    public class Default
    {
        [Header("Tilemap")]
        public NewTilemapData tilemapData;
        public Vector3 gridPosition;

        [Header("Player & Camera")]
        public Vector3 playerPosition;
        public float cameraOrthographicSize;
        public Vector3 cameraPosition;

        [Header("Canon Ball Bounds")]
        public Vector3 canonBallBoundsParentPosition;
        public List<Vector3> canonBallBoundsPositions;

        [Header("Canons")]
        public Vector3 canonObject1Pos;
        public List<Vector3> canonObject1Waypoints;
        public Vector3 canon1WaypointsParentPosition;
        public Vector3 canon1WaypointsHolderScale;


        public Vector3 canonObject2Pos;
        public List<Vector3> canonObject2Waypoints;
        public Vector3 canon2WaypointsParentPosition;
        public Vector3 canon2WaypointsHolderScale;

        public Vector3 canonObject3Pos;
        public List<Vector3> canonObject3Waypoints;
        public Vector3 canon3WaypointsParentPosition;
        public Vector3 canon3WaypointsHolderScale;

        public Vector3 canonObject4Pos;
        public List<Vector3> canonObject4Waypoints;
        public Vector3 canon4WaypointsParentPosition;
        public Vector3 canon4WaypointsHolderScale;
    }

    // ================= MOVEABLE PLATFORM =================
    [System.Serializable]
    public class MoveablePlatform : Default
    {
        [Header("Moveable Platform level")]
        public Vector3 moveablePlatformPosition;
        public Vector3 waypointAPosition;
        public Vector3 waypointBPosition;
    }

    // ================= TELEPORT =================
    [System.Serializable]
    public class Teleport : Default
    {
        [Header("Portal level")]
        public Vector3 teleportEntryPosition;
        public Vector3 teleportExitPosition;
    }
    [System.Serializable]
    public class LimitedMoves : Default 
    {
        [Header("Limited Moves level")]
        public int numberOfMoves;
    }
    [System.Serializable]
    public class LimitedShots : Default 
    {
        [Header("Limited Shots level")]
        public int canon1NumberOfShots;
        public int canon2NumberOfShots;
        public int canon3NumberOfShots;
        public int canon4NumberOfShots;

    }
    [System.Serializable]
    public class Timed : Default
    {
        [Header("Timed level")]
        public float time;
    }

    [System.Serializable]
    public class BombLevel : Default
    {
        [Header("Bomb level")]
        public Vector3 bombPosition;
        //public Sprite bombActivatedSprite;
    }
    [System.Serializable]
    public class ColourMatch : Default
    {
        [SerializeField] public CanonType[] canonTypes=new CanonType[4];
        [SerializeField] public CanonType canon1Type = CanonType.Default;
        [SerializeField] public CanonType canon2Type = CanonType.Default;
        [SerializeField] public CanonType canon3Type = CanonType.Default;
        [SerializeField] public CanonType canon4Type = CanonType.Default;
    }
}
