using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on a script by https://www.youtube.com/watch?v=hxZ7lysQa3o&ab_channel=ExpatStudios

public class Billboard : MonoBehaviour
{
    [SerializeField] Camera targetCam;
    [SerializeField] Transform targetObject;
    [SerializeField] bool ignoreYAxis = true;

    void Start() {
        if (targetCam == null) {
            targetCam = Camera.main;
        }
        if (targetObject == null) {
            targetObject = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // My naive solution commented out, which can result in a Gimbal Lock.
        //targetObject.eulerAngles = new Vector3(targetObject.eulerAngles.x, targetCam.transform.eulerAngles.y, targetObject.eulerAngles.z);

        // A solution by Expat Studios on youtube, in their "BillBoarding Tutorial - Unity" video posted Dec 6,2021.
        Vector3 cameraDirection = targetCam.transform.forward;
        if (ignoreYAxis) {
            cameraDirection.y = 0;
        }
        targetObject.rotation = Quaternion.LookRotation(cameraDirection);
    }
}
