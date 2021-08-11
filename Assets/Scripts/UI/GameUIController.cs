using System.Collections;
using UnityEngine;

public class GameUIController : GameUI
{
    private int destroyedEnemys = 0;

    private void Awake()
    {
        endGamePanel?.SetActive(false);
        settingsPanel?.SetActive(false);
    }
    private void OnEnable()
    {
        GameEvents.OnEnemyDetroy += EnemyDestroy;
        GameEvents.OnPlayerDestroy += UIUpdate;
        GameEvents.OnUIUpdate += UIUpdate;
        GameEvents.OnShowMenu += ShowMenu;
        GameEvents.OnGameEnd += EndGame;
    }
    private void OnDisable()
    {
        GameEvents.OnEnemyDetroy -= EnemyDestroy;
        GameEvents.OnPlayerDestroy -= UIUpdate;
        GameEvents.OnUIUpdate -= UIUpdate;
        GameEvents.OnShowMenu -= ShowMenu;
        GameEvents.OnGameEnd -= EndGame;
    }

    private void ShowMenu()
    {
        if (settingsPanel == null) return;
        settingsPanel.SetActive(!settingsPanel.activeSelf);

        mainSound.value = PlayerPrefs.GetFloat("MainSound");
        effects.value = PlayerPrefs.GetFloat("EffectsSound");
    }
    private void UIUpdate()
    {
        if (SceneData.LevelNum == 0) return;
        UpdatePlayerHealths();
    }
    private void EnemyDestroy()
    {
        HideEnemyIcon(destroyedEnemys);
        destroyedEnemys++;
        if (destroyedEnemys == SceneData.MaxEnemyOnMap)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        if (endGamePanel == null) return;

        endGamePanel.SetActive(true);

        hiScoreCounter.text = SceneData.MaxScore.ToString();
        levelCounter.text = "Уровень " + SceneData.LevelNum.ToString();

        ShowPlayerData(1);
        ShowPlayerData(2);
        SceneData.SetNextLevel();

        StartCoroutine(LoadScene());
    }
    private void ShowPlayerData(int playerNum)
    {
        var playerData = SceneController.instance.GetPlayerData(playerNum);
        if (playerData == null) return;
        string destroy = "Уничтожено: " + playerData.DestroyedEnemy.ToString();
        string score = "Очки: " + playerData.Score.ToString();
       
        if (playerData.Score > SceneData.MaxScore)
        {
            SceneData.UpdateMaxScore(playerData.Score);
            GameEvents.OnNewHiScore?.Invoke();
        }

        UpdateEndGameUI(playerNum, destroy, score);
    }
    #region Coroutines
    private IEnumerator LoadScene()
    {
        while (loadDelay > 0)
        {
            if (returnCounter != null)
            {
                returnCounter.text = loadDelay.ToString();
            }

            yield return new WaitForSecondsRealtime(1);
            loadDelay--;
        }

        SceneLoader.LoadNextScene();
    }
    #endregion
}
