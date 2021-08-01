using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyIcons = new GameObject[20];
    [SerializeField] private TMP_Text player1Health;
    [SerializeField] private TMP_Text player2Health;

    private int destroyedEnemys = 0;

    private void OnEnable()
    {
        GameEvents.OnEnemyDetroy += EnemyDestroy;
        GameEvents.OnPlayerDestroy += UIUpdate;
        GameEvents.OnUIUpdate += UIUpdate;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDetroy -= EnemyDestroy;
        GameEvents.OnPlayerDestroy -= UIUpdate;
        GameEvents.OnUIUpdate -= UIUpdate;
    }

    private void EnemyDestroy()
    {        
        enemyIcons[destroyedEnemys].SetActive(false);
        destroyedEnemys++;
        if (destroyedEnemys == SceneData.MaxEnemyOnMap)
        {
            GameEvents.OnGameEnd?.Invoke();
        }
    }

    private void UIUpdate()
    {
        player1Health.text = SceneController.instance.GetPlayerHealth(1).ToString();
        player2Health.text = SceneController.instance.GetPlayerHealth(2).ToString();
    }
}
