using UnityEngine;

public class Level
{
    private Point[,] map = new Point[26, 26];

    public Level(string mapData)
    {

    }
}

[System.Serializable]
public class Point
{
    public ObjectsTypes type;
}

public enum ObjectsTypes {None, Base, Player1, Player2, Enemy, BrickWall, BroneWall, Grass, Water }