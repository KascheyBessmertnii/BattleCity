using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = new Transform[3];
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private SpawnData[] enemys;
    
    public static int MobsOnMap { get; private set; } = 20;//Default value 20

    private int currentSpawnPoint = 0;
    private int spawnedMobs = 0;

    private void Start()
    {
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

    private GameObject GetRandomEnemy()
    {
        int totalWeight = enemys.Sum(c => c.weight);
        int rnd = Random.Range(0, totalWeight);
        int sum = 0;

        foreach (var item in enemys)
        {
            for (int i = sum; i < item.weight+sum; i++)
            {
                if(i >= rnd)
                {
                    return item.prefab;
                }
            }
            sum += item.weight;
        }

        return enemys.First().prefab;
    }

    IEnumerator SpawnEnemy()
    {
        while(spawnedMobs < MobsOnMap )
        {
            int rnd = (int)(Random.Range(0, (float)enemys.Length));
            GameObject currPrefab = GetRandomEnemy();
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
public class SpawnData
{
    public GameObject prefab;
    public int weight;
}
