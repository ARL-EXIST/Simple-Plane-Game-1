using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : Interactable
{
    public GameObject BrokenRock;

    public PlayerMovement pM;

    public override void Interact ()
    {
        base.Interact();

        Activate();
    }

    private void Activate()
    {
        FindObjectOfType<GameManager>().Damaged();
        
        Destroy(gameObject);
        GameObject BrokenTransform = Instantiate(BrokenRock, transform.position, Random.rotation);
        foreach(Transform child in BrokenTransform.transform){
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody)){
                childRigidBody.constraints = RigidbodyConstraints.None;
                childRigidBody.AddExplosionForce(300f, transform.position, 5f);
            }
        }
        
    }
}
