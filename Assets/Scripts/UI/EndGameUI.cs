using UnityEngine;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TMP_Text hiScoreCounter;
    [SerializeField] private TMP_Text levelCounter;
    [SerializeField] private TMP_Text player1Score;
    [SerializeField] private TMP_Text player1Tanks;
    [SerializeField] private TMP_Text player2Score;
    [SerializeField] private TMP_Text player2Tanks;

    #region Unity methods
    private void Awake()
    {
        if (endGamePanel != null)
            endGamePanel.SetActive(false);
    }
    private void OnEnable()
    {
        GameEvents.OnGameEnd += EndGame;   
    }
    private void OnDisable()
    {
        GameEvents.OnGameEnd -= EndGame;
    }
    #endregion

    #region Private methods
    private void EndGame()
    {
        endGamePanel.SetActive(true);

        hiScoreCounter.text = SceneData.MaxScore.ToString();
        levelCounter.text = "Уровень " + SceneData.LevelNum.ToString();

        ShowPlayerData(1);
        ShowPlayerData(2);
    }
    private void ShowPlayerData(int playerNum)
    {
        var playerData = SceneController.instance.GetPlayerData(playerNum);
        if (playerData == null) return;
        string destroy = "Уничтожено: " + playerData.DestroyedEnemy.ToString();
        string score = "Очки: " + playerData.Score.ToString();

        if(playerNum == 1)
        {
            player1Tanks.text = destroy;
            player1Score.text = score;
        }
        else
        {
            player2Tanks.text = destroy;
            player2Score.text = score;
        }

        if (playerData.Score > SceneData.MaxScore)
            SceneData.UpdateMaxScore(playerData.Score);
    }
    #endregion
}
