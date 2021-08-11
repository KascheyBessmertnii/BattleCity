//RBMovement using rigidbody for move 
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RBMovement : MonoBehaviour, IMovement
{
    private readonly Directions[] directions = new Directions[4] { new Directions(new Vector3(1, 0, 0), 20),
                                                    new Directions(new Vector3(-1, 0, 0), 20),
                                                    new Directions(new Vector3(0, 0, 1), 10),
                                                    new Directions(new Vector3(0, 0, -1), 50) };

    private Rigidbody rb;

    private void Awake()
    {
        TryGetComponent(out rb);

        //Setup default rigidbody
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    private void OnEnable()
    {
        GameEvents.OnGameEnd += Stop;
    }
    private void OnDisable()
    {
        GameEvents.OnGameEnd -= Stop;   
    }
    public void Move(Vector3 direction, float speed)
    {
        if (direction == Vector3.zero)
            direction = Utility.GetRandomValue(directions).Value; //Rotate unit to random direction

        if (direction.magnitude >= 0.1)
        {
            RotateUnit(direction);

            rb.velocity = direction * speed;
        }
        else
            Stop();
    }
    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }
    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }
    private void RotateUnit(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}

[System.Serializable]
public class Directions : IWeighted
{
    public Vector3 Value { get; set; }
    public int Weight { get; set; }

    public Directions(Vector3 dir, int weight)
    {
        Value = dir;
        Weight = weight;
    }
}