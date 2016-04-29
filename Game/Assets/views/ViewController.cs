using UnityEngine;
using UnityEngine.SceneManagement;
using sysObject = System.Object;
using System.Collections;

public class ViewController : Singleton<ViewController>
{
    public static SubView currentSubView;

    private static string lastSceneLoaded;

    

    public static IEnumerator LoadViewInternal<T, U>(string sceneName, T arg, System.Action<U> callback)
    {
        SceneManager.UnloadScene(lastSceneLoaded);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        lastSceneLoaded = sceneName;

        yield return null;

        currentSubView = FindObjectOfType<SubView>(); ;
        if (currentSubView != null)
        {
            ((SubView<T, U>)currentSubView).BaseInit(arg, callback);
        }
        else
        {
            throw new UnityException("Scene name was valid, but no SubView was found");
        }
    }
}

