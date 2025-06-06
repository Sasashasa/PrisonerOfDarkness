using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        StartMenuScene,
        LevelScene,
        LoadingScene
    }
    
    private static Scene _targetScene;

    public static void Load(Scene targetScene)
    {
        _targetScene = targetScene;
        
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        SceneManager.LoadScene(_targetScene.ToString());
    }
}