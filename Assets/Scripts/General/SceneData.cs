using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    #region Fields
    public static int MaxScore { get; private set; }
    public static int LevelNum { get; private set; }
    public static int PlayersNum { get; private set; }
    public static int MaxEnemyOnMap { get; private set; } = 20;

    public static readonly Vector2Int mapSize = new Vector2Int(26, 26);

    //Prefabs
    public static GameObject wallPrefab { get; private set; }
    public static GameObject bronePrefab {get; private set;}
    public static GameObject p1Spawner  {get; private set;}
    public static GameObject p2Spawner  {get; private set;}
    public static GameObject basePrefab { get; private set; }

    public static Dictionary<ObjectsTypes, Vector3> mapSpawnPositions { get; private set; }
    #endregion

    public static void InitializeLevel(int levelNum, int numPlayers)
    {
        LevelNum = levelNum;

        PlayersNum = numPlayers;

        MaxScore = PlayerPrefs.GetInt("MaxScore");

        LoadPrefabs();
    }
    public static void UpdateMaxScore(int newScore)
    {
        MaxScore = newScore;
        PlayerPrefs.SetInt("MaxScore", newScore);
    }
    public static void LoadPrefabs()
    {
        if (wallPrefab != null) return;

        wallPrefab = Resources.Load("Prefabs/Wall") as GameObject;
        bronePrefab = Resources.Load("Prefabs/Brone") as GameObject;
        basePrefab = Resources.Load("Prefabs/Base") as GameObject;
        p1Spawner = Resources.Load("Prefabs/P1SPoint") as GameObject;
        p2Spawner = Resources.Load("Prefabs/P2SPoint") as GameObject;
    }
    public static GameObject GetObjectByType(ObjectsTypes type)
    {
        if (type == ObjectsTypes.BrickWall) return wallPrefab;
        if (type == ObjectsTypes.BroneWall) return bronePrefab;
        if (type == ObjectsTypes.Base) return basePrefab;

        return null;
    }
    public static void FillSpawnPoints()
    {
        mapSpawnPositions = new Dictionary<ObjectsTypes, Vector3>();
        mapSpawnPositions.Add(ObjectsTypes.Enemy1, Vector3.zero);
        mapSpawnPositions.Add(ObjectsTypes.Enemy2, Vector3.zero);
        mapSpawnPositions.Add(ObjectsTypes.Enemy3, Vector3.zero);
        mapSpawnPositions.Add(ObjectsTypes.Player1, Vector3.zero);
        mapSpawnPositions.Add(ObjectsTypes.Player2, Vector3.zero);
    }
    public static void SetSpawPosition(ObjectsTypes type, Vector3 pos)
    {
        if (mapSpawnPositions.ContainsKey(type))
            mapSpawnPositions[type] = pos;
    }
    public static Vector3 GetObjectSpawnPoint(ObjectsTypes type)
    {
        if (mapSpawnPositions.ContainsKey(type))
            return mapSpawnPositions[type];
        return new Vector3(-1, -1, -1);
    }
}
