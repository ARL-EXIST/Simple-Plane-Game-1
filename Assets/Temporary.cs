using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporary : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

}
