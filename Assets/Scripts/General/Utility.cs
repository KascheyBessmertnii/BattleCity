using System.Linq;
using UnityEngine;

public class Utility
{
    public static T GetRandomValue<T>(T[] sourceArray) where T : IWeighted
    {
        int totalWeight = sourceArray.Sum(c => c.Weight);
        int rnd = Random.Range(0, totalWeight);
        int sum = 0;

        foreach (var item in sourceArray)
        {
            for (int i = sum; i < item.Weight + sum; i++)
            {
                if (i >= rnd)
                {
                    return item;
                }
            }
            sum += item.Weight;
        }

        return sourceArray.First();
        }
}

