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

    private PlayerController player1;
    private PlayerController player2;

    private Level level;

    private void OnEnable()
    {
        string data = FileHandler.GetRawlevelData(SceneData.LevelNum);
        level = new Level(SceneData.mapSize, data);

        SpawnPlayer("Player1", level.GetPlayerSpawn(1), 1);
        if (SceneData.PlayersNum == 2)
            SpawnPlayer("Player2", level.GetPlayerSpawn(2), 2);

        //reset players
        GameEvents.OnPlayerDestroy?.Invoke();
    }
    private void OnDestroy()
    {
        instance = null;
    }
    private void SpawnPlayer(string prefabName, Vector3 pos, int playerNum)
    {
        if (pos == new Vector3(-1, -1, -1)) return;
        GameObject prefab = Resources.Load("Prefabs/" + prefabName) as GameObject;
        var obj = Instantiate(prefab, pos, Quaternion.identity).GetComponent<PlayerController>();

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
    public int GetPlayerHealth(int playerNum)
    {
        if (playerNum == 1)
            return player1.CurrentHealth;

        if (playerNum == 2 && player2 != null)
            return player2.CurrentHealth;

        return 0;
    }  
    public bool CanMove(Vector3 rawPosition)
    {
        return false;
    }
}
