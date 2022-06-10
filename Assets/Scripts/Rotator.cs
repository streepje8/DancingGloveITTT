using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Calibrator calibratedInput;
    public Vector3 subtractor = Vector3.zero;
    public void ResetRot()
    {
        subtractor = calibratedInput.calibrated;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(calibratedInput.calibrated - subtractor);
    }
}
