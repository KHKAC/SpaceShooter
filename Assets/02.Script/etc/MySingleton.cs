using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static bool shuttingDown = false;
    static object _lock = new object();
    static T instance;

    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null");
                return null;
            }

            lock (_lock)
            {
                if (instance = null)
                {
                    instance = (T)FindObjectOfType(typeof(T));
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }
    void OnApplicationQuit()
    {
        shuttingDown = true;
    }
    void OnDestroy()
    {
        shuttingDown = true;
    }
}
