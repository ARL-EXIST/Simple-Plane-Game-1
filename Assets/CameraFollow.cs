using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame FixedUpdate
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - 5.34f);
    }
}
