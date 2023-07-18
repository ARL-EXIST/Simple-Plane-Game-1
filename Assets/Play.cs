using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void Activate()
    {
        Invoke("Begin", 0.2f);
    }

    private void Begin()
    {
        SceneManager.LoadScene("Level 01");
    }
}
