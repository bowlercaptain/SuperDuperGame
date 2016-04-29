using UnityEngine;
using sysObject = System.Object;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public abstract class SubView : MonoBehaviour { }

public abstract class SubView<T, U> : SubView
{
    protected System.Action<U> callback;

    public void BaseInit(T arg, System.Action<U> callback)
    {
        this.callback = callback;
        Init(arg);
    }

    protected abstract void Init(T arg);

    protected static void Show(T arg, System.Action<U> callback, string sceneName)
    {
        ViewController.instance.StartCoroutine(ViewController.LoadViewInternal(sceneName, arg, callback));
    }

}