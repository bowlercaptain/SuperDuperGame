using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public void Start()
    {
        StartCoroutine(Login("toaster", "zugzwang"));
        
    }


    //TODO: make this persistent
    private static string sessionCookie;

    public static void sendMove(string gid, string move, string game_url)
    {
        //TODO: send a message
    }

    //public static string[] getHistory

    public IEnumerator Login(string username, string password)
    {
        WWW lolRequest = new WWW("http://superdupergames.org/");
        yield return lolRequest;
        string setCookie = lolRequest.responseHeaders["SET-COOKIE"];
        sessionCookie = setCookie.Substring(0, setCookie.IndexOf(';') + 1);
        Dictionary<string, string> headers = new Dictionary<string, string>();
        string formString = "mode=auth&username=" + username + "&password=" + password;
        byte[] postData = GetBytes(formString);
        Hashtable table = new Hashtable();
        table.Add("cookie", sessionCookie);
        WWW firstRequest = new WWW("http://superdupergames.org/auth.html",postData,table);
        yield return firstRequest;
        //foreach (var key in firstRequest.responseHeaders.Keys)
        //{
        //    Debug.Log(key.ToString());
        //}
        //setCookie = firstRequest.responseHeaders["SET-COOKIE"];
        sessionCookie = setCookie.Substring(0, setCookie.IndexOf(';')+1);
        Debug.Log(firstRequest.text);

     
        Dictionary<string,string> newHeaders = new Dictionary<string,string>();
        newHeaders.Add("COOKIE",sessionCookie);
        Debug.Log(sessionCookie);
       
        WWW secondRequest = new WWW("http://superdupergames.org/main.html",null,newHeaders);

        yield return secondRequest;
        Debug.Log(secondRequest.text);
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
