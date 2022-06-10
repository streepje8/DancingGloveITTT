using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrator : MonoBehaviour
{
    public ArduinoInput input;
    public float treshHold = 1f;

    public Vector3 calibrated = Vector3.zero;

    private Vector3 lastVector = Vector3.zero;
    float delay = 1f;

    // Update is called once per frame
    void Update()
    {
        if(delay >= 0f)
        {
            delay -= Time.deltaTime;
            lastVector = input.arduinoIn;
        } else
        {
            Vector3 difference = input.arduinoIn - lastVector;
            float x = difference.x;
            float y = difference.y;
            float z = difference.z;
            if (Mathf.Abs(x) < treshHold) x = 0;
            if (Mathf.Abs(y) < treshHold) y = 0;
            if (Mathf.Abs(z) < treshHold) z = 0;
            calibrated += new Vector3(x,y,z);
            lastVector = input.arduinoIn;
        }
    }
}
