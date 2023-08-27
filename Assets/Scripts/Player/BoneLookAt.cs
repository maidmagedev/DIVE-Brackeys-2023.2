using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLookAt : MonoBehaviour
{
    public List<Transform> affectedBones; // Reference to the head bone or head parent bone
    public Transform target; // The target to look at
    public float rotationSpeed = 5.0f; // Adjust the speed of rotation

    private void LateUpdate()
    {
        foreach (Transform t in affectedBones) {
            if (t == null || target == null)
                return;

            // Calculate the direction to the target
            Vector3 lookDirection = target.position - t.position;

            // Calculate the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Smoothly rotate the head bone towards the target rotation
            t.rotation = targetRotation;//Quaternion.Slerp(headBone.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}