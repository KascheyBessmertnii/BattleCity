using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour, IDestructable
{
    #region Fields
    [SerializeField] private float speed = 10f;
    [Tooltip("Delay to activation box collider after instantiate projectile")]
    [SerializeField] private float activationDelay = 0.01f;

    private SphereCollider sCollider;
    public int Player { get; private set; }
    #endregion

    #region Unity methods
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    #endregion

    #region public methods
    public void Initialize(PlayerController player)
    {
        Destroy(gameObject, 5f);
        TryGetComponent(out sCollider);
        sCollider.isTrigger = false;
        StartCoroutine(ActivateTrigger());
        if(player != null)
        {
            Player = player.PlayerNum;
        }     
    }
    public void Destroy(int playerNum)
    {
        Destroy(gameObject);
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDestructable obj) && !other.CompareTag("Player"))
        {
            obj.Destroy(Player);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            SoundController.instance.PlayClip();
            Destroy(gameObject);
        }
    }
    #endregion

    #region Coroutines
    IEnumerator ActivateTrigger()
    {
        yield return new WaitForSeconds(activationDelay);
        sCollider.isTrigger = true;
    }
    #endregion
}
