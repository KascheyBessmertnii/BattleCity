using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bonus : MonoBehaviour
{
    public BonusType type;
    public int bonusValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ICollectBonus target))
        {
            target.Collect(this);
            GameEvents.OnRemoveBonus?.Invoke();
            SoundController.instance.PlayCollectBonus();
            Destroy(gameObject);
        }
    }
}

public enum BonusType { Health, Damage }