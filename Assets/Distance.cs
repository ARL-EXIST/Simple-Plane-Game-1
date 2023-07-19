using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Distance : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI distanceText;

    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {
        distanceText.text = player.position.z.ToString("0");
    }
}
