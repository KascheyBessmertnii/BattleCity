using UnityEngine;

public class SceneControllerUI : MonoBehaviour
{
    [Header("Create level section")]
    [SerializeField] protected LevelCreateController createController;
    [SerializeField] protected GameObject createLevelUI;

    [Header("Game section")]
    [SerializeField] protected SceneController gameController;
    [SerializeField] protected GameObject gameUI;
    [SerializeField] protected GameObject endGameUI;

    protected void SetLevel(bool gameScene)
    {
        createController.enabled = !gameScene;
        createLevelUI.SetActive(!gameScene);

        gameController.enabled = gameScene;
        gameUI.SetActive(gameScene);
        //endGameUI.SetActive(gameScene);
    }
}
