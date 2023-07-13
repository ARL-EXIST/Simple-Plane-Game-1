using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speed : MonoBehaviour
{
    public Rigidbody player;
    public TextMeshProUGUI speedText;

    // Update is called once per frame
    private void Update()
    {
        Vector3 forwardVelocity = Vector3.Project(player.velocity, transform.forward);
        float forwardSpeed = forwardVelocity.magnitude;
        speedText.text = (player.velocity.z * 36).ToString("0") + " mph";
    }
}
