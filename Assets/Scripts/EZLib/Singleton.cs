using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _CurrentInstance;
    
    public static T Instance => _CurrentInstance;

    protected virtual void Awake()
    {
        Debug.Assert(_CurrentInstance == null);
        _CurrentInstance = this as T;
        Debug.Assert(_CurrentInstance != null);
    }
}
