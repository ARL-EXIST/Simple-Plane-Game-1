using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unrender : MonoBehaviour
{
    private float distanceFromPlayer;

    private void Update(){
        //if(clone){
        distanceFromPlayer = transform.position.z - GameObject.Find("Main Camera").GetComponent<Transform>().transform.position.z;
            if(distanceFromPlayer < 0){
                Destroy(gameObject);
            }
        //}
    }
}
