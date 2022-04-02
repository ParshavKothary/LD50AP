using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StandaloneInputModuleV2 : StandaloneInputModule
{
    private static StandaloneInputModuleV2 _CurrentInstance;

    public static StandaloneInputModuleV2 Instance => _CurrentInstance;

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(_CurrentInstance == null);
        _CurrentInstance = this;
    }

    public GameObject GetGameObjectUnderPointer(int pointerId)
    {
        var lastPointer = GetLastPointerEventData(pointerId);
        if (lastPointer != null)
            return lastPointer.pointerCurrentRaycast.gameObject;
        return null;
    }

    public GameObject GetGameObjectUnderPointer()
    {
        return GetGameObjectUnderPointer(kMouseLeftId);
    }
}
