using UnityEngine;

public class Shooting : MonoBehaviour, IShooting
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Projectile projectile1Prefab;
    [SerializeField] private float shootDelay = 0.1f;

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

    public void Shoot()
    {
        if (projectile1Prefab == null || shootPoint == null) return;

        if(timer == 0)
        {
            Projectile obj = Instantiate(projectile1Prefab, shootPoint.position, shootPoint.rotation);
            obj.Initialize(player);
            timer = shootDelay;
        }     
    }
}
