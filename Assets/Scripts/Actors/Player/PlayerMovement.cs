using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    [Header("Value Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDrag;
    public int dashStack = 3;

    [Header("References")]
    public Rigidbody rb;
    public Transform orientation;

    [Header("Movement States")]
    public bool isGrounded;
    public bool allowMovement = true;

    [Header("Internal Variables")]    
    private float horizontalInput;
    private float verticalInput;
    Vector3 moveDirection;
    public bool playerHasInput; // is true if the player tried to do inputs, from horizontalInput & verticalInput
    [Header("External References")]
    [SerializeField] PlayerAnimations pAnim;

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
        if (allowMovement) {
            MovePlayer();
        }
    }

    void InputHandler() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        playerHasInput = horizontalInput != 0 || verticalInput != 0;
    }

    
    void MovePlayer() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    // The standard MovePlayer() function is designed to work on each frame.
    public IEnumerator MovePlayerForDuration(Vector3 force, ForceMode forceMode, float duration) {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration) {
            rb.AddForce(force, forceMode);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator MovePlayerForDuration(float speed, float duration) {
        FindObjectOfType<ThirdPersonCamera>().RotateCharacterToForwardCamera();
        float elapsedTime = 0.0f;
        Vector3 direction = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (direction == Vector3.zero) {
            Debug.Log(orientation.forward);
            direction = orientation.forward;
        }

        while (elapsedTime < duration) {
            Debug.Log("moving in " + direction.normalized);
            rb.AddForce(direction.normalized * speed, ForceMode.Force);
            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
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
    public void Dash() {
        if (dashStack > 0) {
            StartCoroutine(DashStackHandler());
            pAnim.FullAnimationReset();
            StartCoroutine(MovePlayerForDuration(500, 0.2f));
            pAnim.animQueue.Enqueue(new PlayerAnimations.AnimState("Dash", 0.667f, 0, true, true, true));
        }
    }
    
    public IEnumerator DashStackHandler() {
        dashStack--;
        yield return new WaitForSeconds(3f);
        if (dashStack < 3) {
            dashStack++;
        }
    }
}
