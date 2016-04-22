using UnityEngine;
using sysObject = System.Object;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


    public abstract class SubView : MonoBehaviour
    {
        protected System.Action<sysObject> callback;
        protected sysObject arg;

        public void Show(sysObject obj, System.Action<sysObject> callback)
        {
            this.arg = obj;
            this.callback = callback;
            Init();
        }

        protected abstract void Init();
    }