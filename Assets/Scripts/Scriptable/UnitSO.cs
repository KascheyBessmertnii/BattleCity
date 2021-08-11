using UnityEngine;

[CreateAssetMenu(menuName = "Battle City/Unit Data")]
public class UnitSO : ScriptableObject
{
    //General
    public UnitType type;
    public float speed;
    public int defaultHealth;
    public int defaultDamage;
    public float shootDelay = 0.7f;
    public Projectile projectilePrefab;

    //Player
    public int playerNum;

    //Enemy
    public bool spawnBonus = false;
    public int scoreReward = 100;
    public float moveTime = 5f;
    
}

public enum UnitType { Player, Enemy }