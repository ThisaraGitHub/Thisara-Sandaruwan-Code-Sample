using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse
{
    // This class is for store data  

    [SerializeField] private float xAxis;                                // Values for xAxis
    [SerializeField] private float yAxis;                                // Values for yAxis

    public Ellipse(float xAxis, float yAxis)                             // Public constructor 
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector2 Evaluate(float t)
    {
        // Calculate all information such as
        // How far the game object alone the ellipse 
        // Y position
        // X position

        float angle = Mathf.Deg2Rad * 360 * t;                            // How far and then convert in to radius 
        float x = Mathf.Sin(angle) * xAxis;                               // X position
        float y = Mathf.Cos(angle) * yAxis;                               // Y position

        return new Vector2(x, y);
    }
}
