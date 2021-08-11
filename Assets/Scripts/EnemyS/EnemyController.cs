using UnityEngine;

public class EnemyController : MonoBehaviour, IDestructable
{
    [SerializeField] private UnitSO unitData;

    private IShooting shooting;
    private IMovement movement;
    private int health;

    private SceneController sceneController;

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
        TryGetComponent(out shooting);
        TryGetComponent(out movement);
        health = unitData.defaultHealth;
        sceneController = SceneController.instance;
    }
    private void Update()
    {
        shooting.Shoot(unitData.projectilePrefab, unitData.shootDelay);
    }
    private void FixedUpdate()
    {
        if (!movement.IsMoving())
        {
            movement.Stop();
            movement.Move(Vector3.zero, unitData.speed);
        }
    }
    #endregion

    #region private methods
    private void EndGame()
    {
        Destroy(gameObject);
    }
    #endregion

    #region public methods
    public void Destroy(int playerNum)
    {
        PlayerController pc = sceneController.GetPlayerData(playerNum);

        if (pc == null) return;

        if(pc.Damage >= health)
        {           
            pc.AddEnemy(unitData.scoreReward);
            Destroy(gameObject);
        }
        else
            health -= pc.Damage;

        if(unitData.spawnBonus)
            GameEvents.OnSpawnBonus?.Invoke();
    }
    #endregion
}