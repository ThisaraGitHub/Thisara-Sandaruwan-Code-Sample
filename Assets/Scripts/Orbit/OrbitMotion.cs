using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    // This class is used to handle the orbit 

    [SerializeField] private Transform orbitingObject;                                               // Reference to the motion of the orbiting object
    public Ellipse orbitPath;                                                                        // Reference to ellipse object

    [Range(0f, 1f)]                                                                                  // Asiginign attributes
    [SerializeField] private float orbitProgress = 0f;                                               // How far long the ellips path 
    [SerializeField] private float orbitPeriod = 3.0f;                                               // How long is gonna take to finsh a one round
    [SerializeField] private bool orbitActive = true;                                                // Pause the orbit if needed 

    // Start is called before the first frame update
    void Start()
    {
        // Check orbitingObject is not null

        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();                                                                     // Set orbiting object position 
        StartCoroutine(AnimateOrbit());                                                                  // Orbit animation
    }

    void SetOrbitingObjectPosition()
    {
        // Evaluate the position should be

        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator AnimateOrbit()
    {
        // Orbit animation

        if (orbitPeriod < 0.1f)
        {
            orbitPeriod = 0.1f;
        }

        float orbitSpeed = 1 / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
