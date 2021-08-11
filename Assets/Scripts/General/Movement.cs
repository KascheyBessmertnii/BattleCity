//Rigidbody velocity move
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Awake()
    {
        TryGetComponent(out rb);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerDestroy += Stop;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerDestroy -= Stop;
    }
    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }
    public void Move(Vector3 direction, float speed)
    {
        if(direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public bool IsMoving()
    {
        throw new System.NotImplementedException();
    }
}
