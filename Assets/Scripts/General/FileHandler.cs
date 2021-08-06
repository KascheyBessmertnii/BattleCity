using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class FileHandler
{
    public static void ToJson<T>(List<T> toSave, string fileName)
    {
        string data = JsonHelper.ToJson<T>(toSave.ToArray());
        Write(data, fileName);
    }

    public static List<T> FromJson<T>(string fileName)
    {
        string loadString = Read(fileName);

        if (string.IsNullOrEmpty(loadString) || loadString == "{}")
        {
            return new List<T>();
        }

        return JsonHelper.FromJson<T>(loadString).ToList();
    }

    private static void Write(string saveString, string fileName)
    {
        File.WriteAllText(Path.Combine("Assets/Resources/Translates", fileName), saveString);
    }

    private static string Read(string fileName)
    {
        string path = Path.Combine("Assets/Resources/Translates", fileName);

        if(File.Exists(path))
        {
            return File.ReadAllText(path);
        }

        return "";
    }

    public static void SavePointsArray(Point[,] toSave, string path)
    {
        string data = "";

        for (int x = 0; x < toSave.GetLength(0); x++)
        {
            for (int y = 0; y < toSave.GetLength(1); y++)
            {
                if(data != "")
                    data += ";";

                data += string.Format("{0}:{1}:{2}:{3}:{4}", x, y, toSave[x, y].PosX, toSave[x, y].PosZ, toSave[x, y].Type);
            }
        }

        File.WriteAllText(path,data);
    }

    public static string GetRawlevelData(int levelNum)
    {
        string path = SceneData.mapFolder + levelNum;
        if(File.Exists(path))
            return File.ReadAllText(path);
        return "";
    }

    public static bool FileExist(string path)
    {
        return File.Exists(path);
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[,] array)
    {
        Wrapper2D<T> wrapper = new Wrapper2D<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    [System.Serializable]
    private class Wrapper2D<T>
    {
        public T[,] Items;
    }
}