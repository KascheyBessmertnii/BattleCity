using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = new Transform[3];
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private UnitsWeight[] units;

    private SpawnData[] enemys;
    
    public static int MobsOnMap { get; private set; } = 20;//Default value 20

    private int currentSpawnPoint = 0;
    private int spawnedMobs = 0;

    private void Start()
    {
        enemys = new SpawnData[units.Length];
        for (int i = 0; i < units.Length; i++)
        {
            enemys[i] = new SpawnData(units[i].prefab, units[i].weight);
        }

        if(spawnPoints.Length > 0)
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
    IEnumerator SpawnEnemy()
    {
        while(spawnedMobs < MobsOnMap )
        {
            GameObject currPrefab = Utility.GetRandomValue<SpawnData>(enemys).Prefab;
            Instantiate(currPrefab, spawnPoints[currentSpawnPoint].position, spawnPoints[currentSpawnPoint].rotation);
            currentSpawnPoint++;
            if (currentSpawnPoint == 3)
                currentSpawnPoint = 0;
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