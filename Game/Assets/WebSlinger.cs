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

        //var client = new RestClient("http://superdupergames.org/auth.html");
        //var request = new RestRequest(Method.POST);
        //request.AddHeader("authorization", "Basic dG9hc3Rlcjp6dWd6d2FuZw==");
        //request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
        //request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"mode\"\r\n\r\nauth\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\ntoaster\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\nzugzwang\r\n-----011000010111000001101001--", ParameterType.RequestBody);
        //IRestResponse response = client.Execute(request);

        
     
        //Dictionary<string, string> headers = new Dictionary<string, string>();
        //string formString = "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"mode\"\r\n\r\nauth\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\ntoaster\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\nzugzwang\r\n-----011000010111000001101001--";
        
        //byte[] postData = GetBytes(formString);
        //Hashtable table = new Hashtable();
        //table.Add("cookie", sessionCookie);
        WWWForm form = new WWWForm();
        form.AddField("mode", "auth");
        form.AddField("username", username);
        form.AddField("password", password);
        Hashtable headers = new Hashtable(form.headers);
        Debug.Log(headers["Content-Type"]);
        headers["Content-Type"]="multipart/form-data";
        WWW firstRequest = new WWW("http://superdupergames.org/auth.html",form.data,headers);
        yield return firstRequest;

        string setCookie = firstRequest.responseHeaders["SET-COOKIE"];
        sessionCookie = setCookie.Substring(0, setCookie.IndexOf(';'));

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
