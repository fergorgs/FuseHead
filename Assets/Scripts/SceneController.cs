using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int TransitionSceneIndex = 0;

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
        int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void LoadSceneWithTransition(int buildIndex)
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Additive).completed += (ctx) => SceneManager.UnloadSceneAsync(lastSceneIndex);
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive).completed += UnloadTransitionScene;
    }

    public void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ReloadActiveSceneWithTransition()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Additive).completed += (ctx) => SceneManager.UnloadSceneAsync(lastSceneIndex);
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive).completed += UnloadTransitionScene;
    }

    private void UnloadTransitionScene(AsyncOperation obj)
    {
        SceneManager.UnloadSceneAsync(TransitionSceneIndex);
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Additive).completed += (ctx) => SceneManager.UnloadSceneAsync(lastSceneIndex);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += UnloadTransitionScene;
    }

    public void LoadNextSceneWithTransition()
    {
        int lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(TransitionSceneIndex, LoadSceneMode.Additive).completed += (ctx) => SceneManager.UnloadSceneAsync(lastSceneIndex);
        SceneManager.LoadSceneAsync(lastSceneIndex + 1, LoadSceneMode.Additive).completed += UnloadTransitionScene;
    }

}
