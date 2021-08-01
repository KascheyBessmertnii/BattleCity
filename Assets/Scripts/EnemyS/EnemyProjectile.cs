using UnityEngine;

public class EnemyProjectile : Projectile
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDestructable obj) && !other.CompareTag("Enemy"))
        {
            obj.Destroy(0);
            Destroy(gameObject);
        }
        else if (!IsIgnore(other.tag))
        {
            Destroy(gameObject);
        }
    }
}
