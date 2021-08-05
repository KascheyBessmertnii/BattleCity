using UnityEngine;
using UnityEngine.UI;

public class StartGameUI : MonoBehaviour
{
    [SerializeField] private Button singlePlay;
    [SerializeField] private Button doublePlay;
    [SerializeField] private Button constructor;

    private void Awake()
    {
        singlePlay.onClick.AddListener(() => PlayerSelected(1));
        doublePlay.onClick.AddListener(() => PlayerSelected(2));
        constructor.onClick.AddListener(() => LoadConstructionLevel());
    }

    private void PlayerSelected(int count)
    {
        SceneData.InitializeLevel(1, count);
        SceneLoader.LoadSceneAsync("GameScene");
    }

    private void LoadConstructionLevel()
    {
        SceneLoader.LoadSceneAsync("ConstructorScene");
    }
}
