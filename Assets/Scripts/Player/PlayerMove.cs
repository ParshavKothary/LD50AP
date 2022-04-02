using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PComponent<Player>
{
    [SerializeField]
    private float moveSpeed;

    void Update()
    {
        if (owner.playerMode != EPlayerMode.Human)
        {
            return;
        }

        owner.gun.transform.rotation = Quaternion.LookRotation(Vector3.forward, (GameCamera.Instance.ScreenToWorldSpacePoint(Input.mousePosition) - transform.position).normalized);

        float xInput = Input.GetKey(KeyCode.D) ? 1.0f : Input.GetKey(KeyCode.A) ? -1.0f : 0.0f;
        float yInput = Input.GetKey(KeyCode.W) ? 1.0f : Input.GetKey(KeyCode.S) ? -1.0f : 0.0f;

        if (xInput == 0.0f && yInput == 0.0f)
        {
            return;
        }

        transform.position += new Vector3(xInput, yInput, 0.0f).normalized * moveSpeed * Time.deltaTime;
    }
}
