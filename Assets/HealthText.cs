using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    private GameManager gM;

    private void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }
    
    private void FixedUpdate()
    {
        healthText.text = "lives: " + (gM.playerHealth).ToString("0");
    }
}
