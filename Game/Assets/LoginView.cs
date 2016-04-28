using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoginView : SubView<bool, string[]> {

    //protected System.Action<sysObject> callback;
    //protected sysObject arg;

    public InputField username;
    public InputField password;

    protected override void Init(bool obj)
    {
        //password.inputType = InputField.InputType.Password;
    }

    public void Submit()
    {
        callback(new string[] { username.text, password.text });
    }
    
    public static void Show(System.Action<string> callback)
    {
        //call base show or viewcontroller show with scenename THIS IS DUMB BULLSHIT FUCK YOU C# DO VTABLE LOOKUPS ON STATICS YOU LAZY ASSHOLES
    }
}
