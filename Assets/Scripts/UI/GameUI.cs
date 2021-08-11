//Script contains all changable UI elements on game scene
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Right panel")]
    [SerializeField] protected GameObject[] enemyIcons = new GameObject[SceneData.MaxEnemyOnMap];
    [SerializeField] protected TMP_Text player1Health;
    [SerializeField] protected TMP_Text player2Health;
    [Header("End level screen")]
    [SerializeField] protected GameObject endGamePanel;
    [SerializeField] protected TMP_Text hiScoreCounter;
    [SerializeField] protected TMP_Text levelCounter;
    [Header("Player 1")]
    [SerializeField] protected TMP_Text player1Score;
    [SerializeField] protected TMP_Text player1Tanks;
    [Header("Player 2")]
    [SerializeField] protected TMP_Text player2Score;
    [SerializeField] protected TMP_Text player2Tanks;
    [Header("Load level")]
    [SerializeField, Range(2f,20f)] protected float loadDelay = 10f;
    [SerializeField] protected TMP_Text returnCounter;
    [Header("Settings screen")]
    [SerializeField] protected GameObject settingsPanel;
    [SerializeField] protected Slider mainSound;
    [SerializeField] protected Slider effects;

    protected void HideEnemyIcon(int num)
    {
        enemyIcons[num].SetActive(false);
    }
    protected void UpdateEndGameUI(int playerNum, string destroyString, string scoreString)
    {
        if(playerNum == 1)
        {
            player1Tanks.text = destroyString;
            player1Score.text = scoreString;
        }
        if(playerNum == 2)
        {
            player2Tanks.text = destroyString;
            player2Score.text = scoreString;
        }

        hiScoreCounter.text = SceneData.MaxScore.ToString();
    }
    protected void UpdatePlayerHealths()
    {
        player1Health.text = SceneController.instance.GetPlayerHealth(1).ToString();
        player2Health.text = SceneController.instance.GetPlayerHealth(2).ToString();
    }
}
