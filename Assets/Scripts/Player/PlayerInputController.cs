using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputController : MonoBehaviour
{
    #region Fields
    [SerializeField] private KeyCode p1ShootKey = KeyCode.Space;
    [SerializeField] private KeyCode p2ShootKey = KeyCode.KeypadEnter;

    private KeyCode soundSettings = KeyCode.P;
    private IMovement movement;
    private IShooting shoot;
    private Vector3 moveDir;
    private PlayerController player;

    private const string p1HorAxis = "P1Horizontal";
    private const string p1VertAxis = "P1Vertical";

    private const string p2HorAxis = "P2Horizontal";
    private const string p2VertAxis = "P2Vertical";
    #endregion

    #region Unity methods
    private void Awake()
    {
        TryGetComponent(out movement);
        TryGetComponent(out shoot);
        TryGetComponent(out player);
    }
    private void Update()
    {
        float x = 0;
        float z = 0;

        if(player.PlayerNum == 1)
        {
            x = Input.GetAxis(p1HorAxis);
            z = Input.GetAxis(p1VertAxis);
        }
        else if(player.PlayerNum == 2)
        {
            x = Input.GetAxis(p2HorAxis);
            z = Input.GetAxis(p2VertAxis);
        }

        if (x != 0)
        {
            moveDir = new Vector3(x, 0, 0);
        }
        else if (z != 0)
        {
            moveDir = new Vector3(0, 0, z);
        }

        Shoot();

        if(Input.GetKeyDown(soundSettings))
        {
            GameEvents.OnShowMenu?.Invoke();
        }
    }

    void FixedUpdate()
    {
        if (movement != null)
        {
            movement.Move(moveDir);
        }
    }
    #endregion

    #region private Methods
    private void Shoot()
    {
        if (shoot != null)
        {
            if ((player.PlayerNum == 1 && Input.GetKeyDown(p1ShootKey)) ||
                (player.PlayerNum == 2 && Input.GetKeyDown(p2ShootKey)))
                shoot.Shoot();
        }
    }
    #endregion
}
