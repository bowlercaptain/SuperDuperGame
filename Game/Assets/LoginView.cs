using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoginView : SubView {

    //protected System.Action<sysObject> callback;
    //protected sysObject arg;

    public InputField username;
    public InputField password;

    protected override void Init()
    {
        password.inputType = InputField.InputType.Password;
    }

    public void Submit()
    {
        callback(new String[] { username.text, password.text });
    }
    

}
