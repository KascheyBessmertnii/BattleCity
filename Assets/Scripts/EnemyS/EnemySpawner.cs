using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints = new Transform[3];
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    
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

    IEnumerator SpawnEnemy()
    {
        while(spawnedMobs < MobsOnMap )
        {
            int rnd = (int)(Random.Range(0, (float)enemyPrefab.Length));
            GameObject currPrefab = enemyPrefab[rnd];
            Instantiate(currPrefab, spawnPoints[currentSpawnPoint].position, spawnPoints[currentSpawnPoint].rotation);
            currentSpawnPoint++;
            if (currentSpawnPoint == 3)
                currentSpawnPoint = 0;
            spawnedMobs++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
