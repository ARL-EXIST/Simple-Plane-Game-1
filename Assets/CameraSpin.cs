using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public GameObject target;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FixedUpdate(){
    transform.LookAt(target.transform);
    transform.Translate(Vector3.right * Time.deltaTime);
    }

}
