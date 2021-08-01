using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameUI : MonoBehaviour
{
    [SerializeField] private Button singlePlay;
    [SerializeField] private Button doublePlay;

    private void Awake()
    {
        singlePlay.onClick.AddListener(() => PlayerSelected(1));
        doublePlay.onClick.AddListener(() => PlayerSelected(2));
    }

    private void PlayerSelected(int count)
    {
        SceneData.InitializeLevel(1, count);
        SceneManager.LoadSceneAsync(1);
    }
}
