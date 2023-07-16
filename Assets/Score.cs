using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public Distance d;
    public TextMeshProUGUI finalScoreText;

    // Update is called once per frame
    private void Awake()
    {
        finalScoreText.text = "Score: " + d.distanceText.text;
    }
}
