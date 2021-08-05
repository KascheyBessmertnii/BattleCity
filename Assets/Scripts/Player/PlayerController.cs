using UnityEngine;

public class PlayerController : MonoBehaviour, IDestructable, ICollectBonus
{
    [SerializeField] private UnitSO unitData;

    public int Damage { get; private set; }
    public int CurrentHealth { get; private set; }
    public int Score { get; private set; } = 0;
    public int DestroyedEnemy { get; private set; } = 0;
    public int PlayerNum => unitData.playerNum;

    private void Awake()
    {
        CurrentHealth = unitData.defaultHealth;
        Damage = unitData.defaultDamage;
    }
    private void RespawnPlayer()
    {
        CurrentHealth--;

        if(CheckEndGame())
        {
            GameEvents.OnGameEnd?.Invoke();
            return;
        }

        if (CurrentHealth == 0) return;

        Vector3 spawnPosition = 
            unitData.playerNum == 1 ? SceneData.GetObjectSpawnPoint(ObjectsTypes.Player1) : SceneData.GetObjectSpawnPoint(ObjectsTypes.Player2);     
        transform.position = spawnPosition;
        GameEvents.OnPlayerDestroy?.Invoke();
        Damage = unitData.defaultDamage;
    }
    private bool CheckEndGame()
    {
        return SceneController.instance.GetPlayerHealth(1) == 0 && SceneController.instance.GetPlayerHealth(2) == 0;
    }
    public void Destroy(int playerNum)
    {
        RespawnPlayer();
    }
    public void AddEnemy(int score)
    {
        DestroyedEnemy++;
        Score += score;
        GameEvents.OnEnemyDetroy?.Invoke();
    }
    public void Collect(Bonus bonus)
    {
        switch (bonus.type)
        {
            case BonusType.Health:
                CurrentHealth += bonus.bonusValue;
                break;
            case BonusType.Damage:
                Damage += bonus.bonusValue;
                break;
            default:
                break;
        }

        GameEvents.OnUIUpdate?.Invoke();
    }
}
