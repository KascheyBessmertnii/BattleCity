using System;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private Point[,] map;

    private const string levelPrefix = "Level";

    private Dictionary<ObjectsTypes, bool> singleObjectSpawned;

    public Level(Vector2Int mapSize, string mapData = null)
    {
        FillSinglePoints();
        SceneData.FillSpawnPoints();

        map = new Point[mapSize.x, mapSize.y];

        if (!string.IsNullOrEmpty(mapData))
        {
            string[] splitData = mapData.Split(';');

            if (splitData.Length == 0) return;

            foreach (string item in splitData)
            {
                string[] splitString = item.Split(':');

                if (splitString.Length == 0) return;
                int.TryParse(splitString[0], out int x);
                int.TryParse(splitString[1], out int y);
                float.TryParse(splitString[2], out float posX);
                float.TryParse(splitString[3], out float posZ);
                Enum.TryParse(splitString[4], out ObjectsTypes type);

                map[x, y] = new Point(posX, posZ, type);

                InstantiateObject(map[x, y]);
            }
        }
        else
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new Point();
                }
            }
        }
    }
    public bool SquareEmpty(int x, int y)
    {
        return map[x, y].Type == ObjectsTypes.None;
    }
    public void SetObject(Vector2Int square, Vector3 pos, ObjectsTypes type)
    {
        map[square.x, square.y] = new Point(pos.x, pos.z, type);
    }
    public void RemoveObject(Vector3 position)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if(map[x,y].PosX == position.x && map[x,y].PosZ == position.z)
                {
                    map[x, y] = new Point();
                }
            }
        }
    }
    public void SaveLevel(string levelNum)
    {
        string path = "Assets/Resources/Levels/" + levelPrefix + levelNum;
        FileHandler.SavePointsArray(map, path);
    }
    public bool Contains(ObjectsTypes type)
    {
        foreach (var item in map)
        {
            if(item.Type == type)
                return true;
        }

        return false;
    }
    public Vector3 GetPlayerSpawn(int player)
    {
        ObjectsTypes type = player == 1 ? ObjectsTypes.Player1 : ObjectsTypes.Player2;

        foreach (var element in map)
        {
            if (element.Type == type)
                return new Vector3(element.PosX, 0, element.PosZ);
        }

        //return position behind the screen
        return new Vector3(-1, -1, -1);
    }
    private void InstantiateObject(Point point)
    {
        Vector3 pos = new Vector3(point.PosX, 0, point.PosZ);
        GameObject prefab = SceneData.GetObjectByType(point.Type);
        if (singleObjectSpawned.ContainsKey(point.Type))
        {
            if (singleObjectSpawned[point.Type]) return;
            singleObjectSpawned[point.Type] = true;
            SceneData.SetSpawPosition(point.Type, pos);
            if (prefab != null)
                UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
        }
        else
        {
            if (prefab != null)
                UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
        }
    }
    private void FillSinglePoints()
    {    
        singleObjectSpawned = new Dictionary<ObjectsTypes, bool>();
        singleObjectSpawned.Add(ObjectsTypes.Base, false);
        singleObjectSpawned.Add(ObjectsTypes.Enemy1, false);
        singleObjectSpawned.Add(ObjectsTypes.Enemy2, false);
        singleObjectSpawned.Add(ObjectsTypes.Enemy3, false);
        singleObjectSpawned.Add(ObjectsTypes.Player1, false);
        singleObjectSpawned.Add(ObjectsTypes.Player2, false);
    }
}

[System.Serializable]
public class Point
{
    public float PosX { get; set; }
    public float PosZ { get; set; }
    public ObjectsTypes Type { get; set; }

    public Point() 
    {
    }

    public Point (float posX, float posZ, ObjectsTypes type)
    {
        PosX = posX;
        PosZ = posZ;
        Type = type;
    }
}

public enum ObjectsTypes {None, Base, Player1, Player2, Enemy1, Enemy2, Enemy3, BrickWall, BroneWall, Grass, Water }