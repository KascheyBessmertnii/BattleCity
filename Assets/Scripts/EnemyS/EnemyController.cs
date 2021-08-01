using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IDestructable
{
    [SerializeField] private float speed;
    [SerializeField] private int health = 1;
    [SerializeField] private bool spawnBonus = false;
    [SerializeField] private int scoreReward = 100;
    [SerializeField] private float moveTime = 5f;
    [SerializeField, Range(0.7f, 2f)] private float shootInterval = 0.7f;

    private readonly Vector3[] directions = new Vector3[4] { new Vector3(1, 0, 0),
                                                    new Vector3(-1, 0, 0),
                                                    new Vector3(0, 0, 1),
                                                    new Vector3(0, 0, -1) };

    private Rigidbody rb;
    private IShooting shooting;

    private bool isDestroy = false;
    private Vector3 currentDirection = Vector3.zero;
    private float shootTimer = 0;

    #region Unity methods
    private void OnEnable()
    {
        GameEvents.OnGameEnd += EndGame;
    }
    private void OnDisable()
    {
        GameEvents.OnGameEnd -= EndGame;
    }
    private void Awake()
    {
        TryGetComponent(out rb);
        TryGetComponent(out shooting);
    }
    private void Update()
    {
        if (shootTimer <= 0)
            TryShoot();
        else
            shootTimer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (isDestroy) return;

        if (currentDirection == Vector3.zero)
        {
            StopAllCoroutines();
            StartCoroutine(Move());
        }
            
        if (rb.velocity.magnitude < speed - 0.5f)
        {
            StopAllCoroutines();
            currentDirection = Vector3.zero;
        }
    }
    #endregion

    #region private methods
    private void TryShoot()
    {
        float rnd = Random.Range(0, 1f);

        if(rnd > 0.8f)
        {
            shootTimer = shootInterval;
            shooting.Shoot();
        }
    }
    private Vector3 GetRandomDirections()
    {
        int rnd = Random.Range(0, directions.Length);

        return directions[rnd];
    }
    private void Stop()
    {
        currentDirection = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
    private void EndGame()
    {
        Destroy(gameObject);
        isDestroy = true;
        Stop();
    }
    #endregion

    #region public methods
    public void Destroy(int playerNum)
    {
        PlayerController pc = SceneController.instance.GetPlayerData(playerNum);

        if (pc == null) return;

        if(pc.Damage >= health)
        {           
            pc.AddEnemy(scoreReward);
            SoundController.instance.PlayDestroy();
            Destroy(gameObject);
            isDestroy = true;
            Stop();
        }
        else
            health -= pc.Damage;

        if(spawnBonus)
            GameEvents.OnSpawnBonus?.Invoke();
    }
    #endregion

    #region Coroutines
    IEnumerator Move()
    {
        currentDirection = GetRandomDirections();
        float targetAngle = Mathf.Atan2(currentDirection.x, currentDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        float elapsedTime = moveTime;
        rb.velocity = currentDirection * speed;
        while (elapsedTime > 0)
        {       
            elapsedTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Stop();
    }
    #endregion
}
