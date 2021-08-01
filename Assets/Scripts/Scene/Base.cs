using UnityEngine;

public class Base : MonoBehaviour, IDestructable
{
    private SoundController sc;

    private void Awake()
    {
        sc = SoundController.instance;
    }

    public void Destroy(int playerNum)
    {
        sc.PlayBaseDestroy();
        GameEvents.OnGameEnd?.Invoke();
        Destroy(gameObject);
    }
}
