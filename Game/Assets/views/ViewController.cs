using UnityEngine;
using UnityEngine.SceneManagement;
using sysObject = System.Object;
using System.Collections;

    public class ViewController : Singleton<ViewController>
    {
        public static SubView currentSubView;

        private static string lastSceneLoaded;

        public static void LoadView(string sceneName, sysObject obj, System.Action<sysObject> callback)
        {
            instance.StartCoroutine(LoadViewInternal(sceneName, obj, callback));
        }

        private static IEnumerator LoadViewInternal(string sceneName, sysObject obj, System.Action<sysObject> callback)
        {
            SceneManager.UnloadScene(lastSceneLoaded);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            lastSceneLoaded = sceneName;

            yield return null;

            currentSubView = FindObjectOfType<SubView>(); ;
            if (currentSubView != null)
            {
                currentSubView.Show(obj, callback);
            }
            else
            {
                throw new UnityException("Scene name was valid, but no SubView was found");
            }
        }
    }

