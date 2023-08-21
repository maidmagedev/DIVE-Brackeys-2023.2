using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Value Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDrag;

    [Header("References")]
    public Rigidbody rb;
    public Transform orientation;

    [Header("Movement States")]
    public bool isGrounded;

    [Header("Internal Variables")]    
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        SpeedLimiter();
        DragHandler();
    }

    // Physics calaculations
    void FixedUpdate() {
        MovePlayer();
    }

    void InputHandler() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


    }

    
    void MovePlayer() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    // Checks to see if the player's flat movement speed exceeds maximum. If true, sets the flat movement speed back to maximum.
    void SpeedLimiter() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // this vector isolates the flat velocity values, only X and Z, and ignores any vertical values.

        if (flatVel.magnitude > moveSpeed) { // if flat movement speed exceeds maximum speed...
            Vector3 limitedVel = flatVel.normalized * moveSpeed; // move speed is set to the maximum speed, in the direction of travel.
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z); // limitedVel combined with pre-existing vertical velocity.
        }
    }

    void DragHandler() {
        if (isGrounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }
}
