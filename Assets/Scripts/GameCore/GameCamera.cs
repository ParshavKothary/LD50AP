using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCamera : Singleton<GameCamera>
{
    private Vector2 ScreenSize;

    private Vector3 HalfScreenOffset;
    private Vector3 InvScreenHeight;

    private Vector3 PrevMousePosition;
    private bool bDragging;

    public Camera MainCamera { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        MainCamera = GetComponent<Camera>();
        Debug.Assert(MainCamera != null);
        Debug.Assert(MainCamera.orthographic); // 2D only!
    }

    protected virtual void Start()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
        OnScreenResize();
    }

    protected virtual void Update()
    {
        if (Screen.width != ScreenSize.x || Screen.height != ScreenSize.y)
        {
            OnScreenResize();
        }

        //ProcessInput();
        //HandleDrag();
    }

    protected virtual void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0) && GetGameObjectUnderMouse() == null)
        {
            bDragging = true;
            PrevMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            bDragging = false;
        }

        if (Input.mouseScrollDelta.y != 0.0f)
        {
            MainCamera.orthographicSize -= Input.mouseScrollDelta.y * 0.2f;
        }
    }

    protected virtual void HandleDrag()
    {
        if (bDragging == false)
        {
            return;
        }

        transform.position -= ScreenToWorldSpaceDirection(Input.mousePosition - PrevMousePosition);
        PrevMousePosition = Input.mousePosition;
    }

    private void OnScreenResize()
    {
        HalfScreenOffset = new Vector3(Screen.width, Screen.height, 0.0f) * -0.5f;
        InvScreenHeight = new Vector3(1.0f / Screen.height, 1.0f / Screen.height, 0.0f);
    }

    public Vector3 GetZeroPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, 0.0f);
    }

    public Vector3 ScreenToWorldSpacePoint(Vector3 ScreenSpacePoint)
    {
        return GetZeroPosition() + (Vector3.Scale(ScreenSpacePoint + HalfScreenOffset, InvScreenHeight) * MainCamera.orthographicSize * 2.0f);
    }

    public Vector3 ScreenToWorldSpaceDirection(Vector3 ScreenSpaceDir)
    {
        return Vector3.Scale(ScreenSpaceDir, InvScreenHeight) * MainCamera.orthographicSize * 2.0f;
    }

    public GameObject GetGameObjectUnderMouse()
    {
        GameObject UIObject = StandaloneInputModuleV2.Instance.GetGameObjectUnderPointer();
        if (UIObject == null)
        {
            Collider[] WorldObjectColliders = Physics.OverlapSphere(ScreenToWorldSpacePoint(Input.mousePosition), 0.01f); // Because orthographic camera
            if (WorldObjectColliders.Length > 0)
            {
                Debug.Log(WorldObjectColliders[0].gameObject.name);
                return WorldObjectColliders[0].gameObject;
            }
        }

        return UIObject;
    }
}
