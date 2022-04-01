using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PComponent<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T owner;

    protected virtual void Awake()
    {
        owner = GetComponent<T>();
        Debug.Assert(owner != null);
    }
}
