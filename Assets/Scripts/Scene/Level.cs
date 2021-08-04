using UnityEngine;

public class Level
{
<<<<<<< Updated upstream
    private Point[,] map = new Point[26, 26];

    public Level(string mapData)
    {

=======
    private Point[,] map;
    private readonly Vector3 topLeft = new Vector3(-100f, 8f, 100f);
    private readonly Vector3 basePosition = new Vector3(0, 0, -96f);

    public Level(Vector2Int mapSize, string mapData = null)
    {
        map = new Point[mapSize.x, mapSize.y];

        if(mapData != null)
        {
            string[] splitData = mapData.Split(';');

            if (splitData.Length == 0) return;
        }

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new Point();
            }
        }
    }

    public bool SquareEmpty(int x, int y)
    {
        return map[x, y].type == ObjectsTypes.None;
    }

    public void SetObject(int x, int y, ObjectsTypes type)
    {
        map[x, y].type = type;
>>>>>>> Stashed changes
    }
}

[System.Serializable]
public class Point
{
<<<<<<< Updated upstream
    public ObjectsTypes type;
=======
    public float PosX { get; set; }
    public float PosY { get; set; }
    public ObjectsTypes type { get; set; }
>>>>>>> Stashed changes
}

public enum ObjectsTypes {None, Base, Player1, Player2, Enemy, BrickWall, BroneWall, Grass, Water }