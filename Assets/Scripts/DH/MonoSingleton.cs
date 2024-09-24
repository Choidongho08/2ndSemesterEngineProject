using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsDestroy = false;

    public static T instance
    {
        get
        {
            if (IsDestroy)
            {
                _instance = null;
            }
            if( _instance == null )
            {
                _instance = FindObjectOfType<T>();

                if( _instance == null )
                {
                    Debug.Log($"error code : {typeof(T).Name} singleton is none");
                }
                else
                {
                    IsDestroy = false;
                }

            }
            return _instance;
        }
    }
    private void OnDisable()
    {
        IsDestroy = true;
    }
}
