using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IDestructable
{
    [SerializeField] private UnitSO unitData;

    private readonly Directions[] directions = new Directions[4] { new Directions(new Vector3(1, 0, 0), 20),
                                                    new Directions(new Vector3(-1, 0, 0), 20),
                                                    new Directions(new Vector3(0, 0, 1), 10),
                                                    new Directions(new Vector3(0, 0, -1), 30) };

    private Rigidbody rb;
    private IShooting shooting;
    private int health;

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
        health = unitData.defaultHealth;
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
            Move();
        }
            
        if (rb.velocity.magnitude < unitData.speed - 0.1f)
        {
            Stop();
        }
    }
    #endregion

    #region private methods
    private void TryShoot()
    {
        float rnd = Random.Range(0, 1f);

        if(rnd > 0.9f)
        {
            shootTimer = unitData.shootInterval;
            shooting.Shoot();
        }
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

    private void Move()
    {
        currentDirection = Utility.GetRandomValue<Directions>(directions).Direct;
        float targetAngle = Mathf.Atan2(currentDirection.x, currentDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        rb.velocity = currentDirection * unitData.speed;
    }
    #endregion

    #region public methods
    public void Destroy(int playerNum)
    {
        PlayerController pc = SceneController.instance.GetPlayerData(playerNum);

        if (pc == null) return;

        if(pc.Damage >= health)
        {           
            pc.AddEnemy(unitData.scoreReward);
            SoundController.instance.PlayDestroy();
            Destroy(gameObject);
            isDestroy = true;
            Stop();
        }
        else
            health -= pc.Damage;

        if(unitData.spawnBonus)
            GameEvents.OnSpawnBonus?.Invoke();
    }
    #endregion
}

[System.Serializable]
public class Directions : IWeighted
{
    public Vector3 Direct { get; set; }
    public int Weight { get; set; }

    public Directions(Vector3 dir, int weight)
    {
        Direct = dir;
        this.Weight = weight;
    }
}