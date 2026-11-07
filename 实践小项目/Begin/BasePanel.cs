using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this as T;
    }

    public  virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
}
