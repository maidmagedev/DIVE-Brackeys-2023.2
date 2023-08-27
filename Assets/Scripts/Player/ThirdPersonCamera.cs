using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Manipulates orientation to match the viewing direction of the camera.
// This script rotates the player to always be facing towards their moving direction.
// This script allows the camera to zoom in & out with a maximum range defined by fields.
//
// cant remember if I wrote this script or not.

public class ThirdPersonCamera : MonoBehaviour
{

    [Header("Field of View Bounds")]
    [SerializeField] float lowerFOVLimit = 15; // lowest FOV possible via scroll.
    [SerializeField] float upperFOVLimit = 90; // maximum FOV possible via scroll.

    [Header("References")]
    public Transform orientation;
    public Transform playerBody;
    public Transform playerObj;
    public Rigidbody rb;
    public CinemachineFreeLook cam;
    
    public float bodyRotationSpeed;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        Vector3 viewDir = playerBody.position - new Vector3(transform.position.x, playerBody.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Changes the player object's forward direction towards the camera's facing direction. This also rotates the player.
        if (inputDir != Vector3.zero) {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * bodyRotationSpeed);
        }

        // Mouse Scroll Wheel Camera Zoom
        if (Input.mouseScrollDelta.y < 0) {
            cam.m_Lens.FieldOfView += 5;
        } else if (Input.mouseScrollDelta.y > 0) {
            cam.m_Lens.FieldOfView -= 5;
        }

        // FOV limits.
        if (cam.m_Lens.FieldOfView > upperFOVLimit) {
            cam.m_Lens.FieldOfView = upperFOVLimit;
        }
        if (cam.m_Lens.FieldOfView < lowerFOVLimit) {
            cam.m_Lens.FieldOfView = lowerFOVLimit;
        }
    }

    public void RotateCharacterToForwardCamera() {
        Vector3 viewDir = playerBody.position - new Vector3(transform.position.x, playerBody.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;
        playerObj.forward = orientation.forward;
    }
}
