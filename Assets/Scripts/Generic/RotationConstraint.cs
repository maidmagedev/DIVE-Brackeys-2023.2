using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationConstraint : MonoBehaviour
{
    [SerializeField] Vector3 desiredRotations;
    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;

    // Update is called once per frame
    void Update()
    {
        float newX = transform.localEulerAngles.x;
        float newY = transform.localEulerAngles.y;
        float newZ = transform.localEulerAngles.z;

        if (x) {
            newX = desiredRotations.x;
        }
        if (y) {
            newY = desiredRotations.y;
        }
        if (z) {
            newZ = desiredRotations.z;
        }

        transform.localEulerAngles = new Vector3(newX, newY, newZ);
    }
}
