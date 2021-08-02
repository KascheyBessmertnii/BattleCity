using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void LoadSceneAsync(int sceneIndex, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadSceneAsync(sceneIndex, sceneMode);
    }

    public static void LoadSceneAsync(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadSceneAsync(sceneName, sceneMode);
    }
}
