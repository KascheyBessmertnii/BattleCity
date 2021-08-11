using UnityEngine;

public class Shooting : MonoBehaviour, IShooting
{
    [SerializeField] private Transform shootPoint;

    private float timer = 0;
    private PlayerController player;

    private void Awake()
    {
        TryGetComponent(out player);
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
                timer = 0;
        }
    }

    public void Shoot(Projectile prefab, float shootInterval)
    {
        if (shootPoint == null) return;

        if (timer == 0)
        {
            Projectile obj = Instantiate(prefab, shootPoint.position, shootPoint.rotation);
            obj.Initialize(player);
            timer = shootInterval;
        }
    }
}
