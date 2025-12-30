using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Scriptable Objects/LevelSO")]
public class LevelSO : ScriptableObject
{
    public enum LevelType
    {
        Default,
        MoveablePlatform,
        Teleports,
    }

    public LevelType levelType;

    [SerializeReference] public Default levelData;

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
        [Header("Moveable Platform")]
        public Vector3 moveablePlatformPosition;
        public Vector3 waypointAPosition;
        public Vector3 waypointBPosition;
    }

    // ================= TELEPORT =================
    [System.Serializable]
    public class Teleport : Default
    {
        [Header("Teleport")]
        public Vector3 teleportEntryPosition;
        public Vector3 teleportExitPosition;
    }
}
