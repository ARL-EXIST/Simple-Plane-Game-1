using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Activate()
    {
        Invoke("Menu", 0.2f);
    }

    private void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
