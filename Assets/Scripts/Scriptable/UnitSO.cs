using UnityEngine;

[CreateAssetMenu(menuName = "Battle City/Unit Data")]
public class UnitSO : ScriptableObject
{
    public UnitType type;
    public float speed;
    public int defaultHealth;
    public int defaultDamage;

    //Player
    public int playerNum;

    //Enemy
    public bool spawnBonus = false;
    public int scoreReward = 100;
    public float moveTime = 5f;
    public float shootInterval = 0.7f;
}

public enum UnitType { Player, Enemy }