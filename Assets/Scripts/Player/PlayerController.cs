using UnityEngine;

public class PlayerController : MonoBehaviour, IDestructable, ICollectBonus
{
    [SerializeField] private int startHealth = 3;
    [SerializeField] private int defaultDamage = 1;
    [SerializeField, Range(1,2)] private int playerNum = 1;

    public int Damage { get; private set; }
    public int CurrentHealth { get; private set; }
    public int Score { get; private set; } = 0;
    public int DestroyedEnemy { get; private set; } = 0;
    public int PlayerNum => playerNum;

    private void Awake()
    {
        CurrentHealth = startHealth;
        Damage = defaultDamage;
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

        Transform spawnPosition = 
            playerNum == 1 ? SceneController.instance.player1Spawn : SceneController.instance.player2Spawn;     
        transform.position = spawnPosition.position;
        transform.rotation = spawnPosition.rotation;
        GameEvents.OnPlayerDestroy?.Invoke();
        Damage = defaultDamage;
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
