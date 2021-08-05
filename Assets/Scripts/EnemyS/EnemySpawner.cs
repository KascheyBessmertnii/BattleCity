using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private UnitsWeight[] units;

    private SpawnData[] enemys;
    
    public static int MobsOnMap { get; private set; } = 20;//Default value 20

    private int currentSpawnPoint = 1;
    private int spawnedMobs = 0;

    private void Start()
    {
        enemys = new SpawnData[units.Length];
        for (int i = 0; i < units.Length; i++)
        {
            enemys[i] = new SpawnData(units[i].prefab, units[i].weight);
        }

        StartCoroutine(SpawnEnemy());
    }
    private void OnEnable()
    {
        GameEvents.OnGameEnd += GameEnd;
    }
    private void OnDisable()
    {
        GameEvents.OnGameEnd -= GameEnd;
    }
    private void GameEnd()
    {
        StopAllCoroutines();
    }

    private Vector3 GetSpawnPositionByNum(int num)
    {
        if (num == 1)
            return SceneData.GetObjectSpawnPoint(ObjectsTypes.Enemy1);
        if (num == 2)
            return SceneData.GetObjectSpawnPoint(ObjectsTypes.Enemy2);
        if (num == 3)
            return SceneData.GetObjectSpawnPoint(ObjectsTypes.Enemy3);
        return new Vector3(-1, -1, -1);
    }
    IEnumerator SpawnEnemy()
    {
        while(spawnedMobs < MobsOnMap )
        {
            GameObject currPrefab = Utility.GetRandomValue<SpawnData>(enemys).Prefab;
            Vector3 pos = GetSpawnPositionByNum(currentSpawnPoint);
            if(pos != new Vector3(-1,-1,-1))
            {
                Instantiate(currPrefab, pos, Quaternion.identity);
            }
            
            currentSpawnPoint++;
            if (currentSpawnPoint == 4)
                currentSpawnPoint = 1;
            spawnedMobs++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    
}

[System.Serializable]
public class SpawnData : IWeighted
{
    public GameObject Prefab { get; set; }
    public int Weight { get; set; }

    public SpawnData(GameObject prefab, int weight)
    {
        Prefab = prefab;
        Weight = weight;
    }
}

[System.Serializable]
public class UnitsWeight
{
    public GameObject prefab;
    public int weight;
}