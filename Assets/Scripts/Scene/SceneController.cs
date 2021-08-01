using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singleton
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    public Transform player1Spawn;
    public Transform player2Spawn;

    private PlayerController player1;
    private PlayerController player2;

    private void Start()
    {
        SpawnPlayer("Player1", player1Spawn, 1);
        if (SceneData.PlayersNum == 2)
            SpawnPlayer("Player2", player2Spawn, 2);

        GameEvents.OnPlayerDestroy?.Invoke();
    }

    private void SpawnPlayer(string prefabName, Transform pos, int playerNum)
    {
        GameObject prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
        var obj = Instantiate(prefab, pos.position, pos.rotation).GetComponent<PlayerController>();

        if (playerNum == 1)
            player1 = obj;
        else
            player2 = obj;
    }

    public PlayerController GetPlayerData(int playerNum)
    {
        if (playerNum == 0)
            return null;

        if (playerNum == 1)
            return player1;

        return player2;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
