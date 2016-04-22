using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance = null;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    _instance = (new GameObject(typeof(T).Name)).AddComponent<T>();
                }
                _instance.Init();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public virtual void Init() { }
    

    public virtual void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            Destroy(gameObject);
        }
        else if (_instance == null)
        {
            _instance = (T)this;
            Init();
        }
    }


}