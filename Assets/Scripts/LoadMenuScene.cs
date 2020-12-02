using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenuScene : MonoBehaviour
{
    public static bool menuLoaded = false;
#if !UNITY_EDITOR
    void Start()
    {
        if(!menuLoaded)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single).completed += (ctx) => menuLoaded = true;
        }
    }
#endif
}
