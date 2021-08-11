using UnityEngine;

public interface IMovement
{
    void Move(Vector3 direction, float speed);
    void Stop();
    bool IsMoving();
}
