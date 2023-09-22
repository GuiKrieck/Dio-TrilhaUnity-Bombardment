using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float degreesPerSecond = 90f;

    // Update is called once per frame
    void Update()
    {
        float stepY = degreesPerSecond * Time.deltaTime;
        transform.Rotate(0, -stepY, 0);        
    }
}
