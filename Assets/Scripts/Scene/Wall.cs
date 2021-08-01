using UnityEngine;

public class Wall : MonoBehaviour, IDestructable
{
    [SerializeField] private int health = 1;
    public void Destroy(int playerNum)
    {
        int damage = playerNum == 0 ? 1 : SceneController.instance.GetPlayerData(playerNum).Damage;

        if (damage >= health)
        {
            Destroy(gameObject);
        }
        else
            SoundController.instance.PlayClip();
    }
}
