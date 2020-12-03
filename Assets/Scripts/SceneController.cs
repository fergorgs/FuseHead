using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int TransitionSceneIndex = 0;

    public bool IsNextLevelAvailable()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string[] sceneNameParts = currentSceneName.Split('_');

        string nextSceneName = sceneNameParts[0] + '_' + (int.Parse(sceneNameParts[1]) + 1).ToString("00") + ".unity";

        string currentScenePath = SceneManager.GetActiveScene().path;
        string scenePath = currentScenePath.Substring(0, currentScenePath.LastIndexOf('_')-2) + nextSceneName;

        return SceneUtility.GetBuildIndexByScenePath(scenePath) != -1;
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadNextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadActiveScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneWithTransition(int buildIndex)
    {
        // Load Transition Scene Single mode (unloads all active scenes)
        // Load new scene Async on completed
        // Unload transition scene on completed
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var transitionSceneOperation = SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Single);

        transitionSceneOperation.completed += (obj) => 
        {
            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive).completed += UnloadTransitionScene;   
        };
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var transitionSceneOperation = SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Single);

        transitionSceneOperation.completed += (obj) =>
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += UnloadTransitionScene;
        };
    }

    public void LoadNextSceneWithTransition()
    {
        LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadActiveSceneWithTransition()
    {
        LoadSceneWithTransition(SceneManager.GetActiveScene().buildIndex);
    }

    private void UnloadTransitionScene(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync(TransitionSceneIndex);
    }
}
