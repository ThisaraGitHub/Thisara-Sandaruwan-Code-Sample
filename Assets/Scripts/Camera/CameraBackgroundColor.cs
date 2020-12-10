using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundColor : MonoBehaviour
{
    // This class is used to change the backgroundcolor
    // This is not listed on the test, but added to give a nice look

    [SerializeField] private Color color1 = Color.red;               // Reference to the starting color
    [SerializeField] private Color color2 = Color.blue;              // Reference to the end color
    [SerializeField] private float duration = 3.0F;                  // Duration for color change
    [SerializeField] private Camera cam;                             // Reference to the main camera

    void Start()
    {
        // Asigning cameara nad clear flags
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        // PingPongs the value and it will not go beyond the duration
        float t = Mathf.PingPong(Time.time, duration) / duration;
        cam.backgroundColor = Color.Lerp(color1, color2, t);
    }
}
