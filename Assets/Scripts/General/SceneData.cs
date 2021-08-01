using UnityEngine;

public class SceneData
{
    #region Fields
    public static int MaxScore { get; private set; }
    public static int LevelNum { get; private set; }
    public static int PlayersNum { get; private set; }
    public static int MaxEnemyOnMap { get; private set; } = 20;
    #endregion

    public static void InitializeLevel(int levelNum, int numPlayers)
    {
        LevelNum = levelNum;

        PlayersNum = numPlayers;

        MaxScore = PlayerPrefs.GetInt("MaxScore");
    }
    public static void UpdateMaxScore(int newScore)
    {
        MaxScore = newScore;
        PlayerPrefs.SetInt("MaxScore", newScore);
    }
}
