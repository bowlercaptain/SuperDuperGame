using UnityEngine;
using System.Collections;
using System;

public class NavigationManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!WebSlinger.IsLoggedIn())
        {
            ViewController.LoadView("loginPage", null, (System.Object report) => { RecieveLogin((String[])report); });
        }
        else
        {
            ShowMain();
        }
    }

    void RecieveLogin(string[] data)
    {
        StartCoroutine(RecieveLoginr(data));
    }

    IEnumerator RecieveLoginr(string[] data)
    {
        yield return StartCoroutine(WebSlinger.Login(data[0], data[1]));
        ShowMain();
    }
    

    void ShowMain()
    {
        Debug.Log("Main screen turn on");
        StartCoroutine(WebSlinger.sendMove("29636", "c3-d4", "play_mchess"));
        StartCoroutine(WebSlinger.sendMove("29636", "d4-c3", "play_mchess"));
        Debug.Log("Defs sent");
    }
}
