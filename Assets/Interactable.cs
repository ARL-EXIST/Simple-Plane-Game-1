using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public bool clone = false;
    private bool triggered = false;

    public virtual void Interact ()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void OnTriggerEnter(Collider colliderInfo)
    {
        if(!triggered)
        {
            Interact();
            triggered = true;
        }
    }

}
