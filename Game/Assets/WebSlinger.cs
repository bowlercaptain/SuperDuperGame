using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WebSlinger : MonoBehaviour {

    private static WebSlinger _instance;
    
    private static WebSlinger instance{
        get{
            if(_instance == null){
                _instance = new GameObject("Webslinger", typeof(WebSlinger)).GetComponent<WebSlinger>();
            }
            return _instance;
        }
    }

    public void Awake()
    {

    }

    public IEnumerator Start()
    {
        
        Debug.Log("Starting");
        //if (!IsLoggedIn()) {
            Debug.Log("No cookie, logging in");
            yield return StartCoroutine(Login("toaster", "zugzwang"));
        //}
        Debug.Log("logged in");
        yield return StartCoroutine(sendMove("29636", "c3-d4", "play_mchess"));
        yield return StartCoroutine(sendMove("29636", "d4-c3", "play_mchess"));
        Debug.Log("Defs sent");
    }

    bool IsLoggedIn()
    {
        return sessionCookie != null && DateTime.Now < sessionCookieExpiry;
    }

    private string sessionCookie { get { return PlayerPrefs.GetString("loginCookie", null); } set { PlayerPrefs.SetString("loginCookie", value); }  }
    private DateTime sessionCookieExpiry { get { return DateTime.Parse(PlayerPrefs.GetString("loginCookieExpiry", null)); } set { PlayerPrefs.SetString("loginCookieExpiry", value.ToString()); } }

    public IEnumerator Login(string username, string password)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("mode", "auth");
        form.AddField("username", username);
        form.AddField("password", password);
        Dictionary<string, string> headers = new Dictionary<string, string>(form.headers);
        WWW firstRequest = new WWW("http://superdupergames.org/auth.html", form.data, headers);
        yield return firstRequest;

        string setCookie = firstRequest.responseHeaders["SET-COOKIE"];
        string cookieExpirationStr = setCookie.Substring(setCookie.IndexOf("expires="));
        sessionCookie = setCookie.Substring(0, setCookie.IndexOf(';') + 1);
        sessionCookieExpiry = DateTime.Parse(cookieExpirationStr.Substring(cookieExpirationStr.IndexOf(",")));
    }

    public IEnumerator sendMove(string gid, string move, string game_url)
    {
        Dictionary<string, string> newHeaders = new Dictionary<string, string>();
        newHeaders.Add("COOKIE", sessionCookie);
        WWW moveRequest = new WWW("http://superdupergames.org/main.html?page=" + game_url + "&num=" + gid + "&mode=move&fullmove=" + move, null, newHeaders);
        yield return moveRequest;
        Debug.Log(moveRequest.text);
    }

    static byte[] GetBytes(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    static string GetString(byte[] bytes)
    {
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }
}
