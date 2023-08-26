using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    public Transform headBone; // Reference to the head bone or head parent bone
    public Transform target; // The target to look at
    public float rotationSpeed = 5.0f; // Adjust the speed of rotation

    private void Update()
    {
        if (headBone == null || target == null)
            return;

        // Calculate the direction to the target
        Vector3 lookDirection = target.position - headBone.position;

        // Calculate the rotation towards the target
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        // Smoothly rotate the head bone towards the target rotation
        headBone.rotation = Quaternion.Slerp(headBone.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}