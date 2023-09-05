using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

// Mainly a test script, to be replaced or revamped once systems are in place
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerMovement playerMovement; 
    public int attackState = 1;
    public bool atIdle = true;
    public bool isInAction = false;
    private bool allowBufferInput = false; // if true, allows the player to queue up the next move in an attack string.
    public bool allowAnimationCancel = false; // allows this animation to be overridden.
    public int debugqueuesize;
    public bool meleeMode = true;
    [SerializeField] GameObject gunLayer1;
    [SerializeField] GameObject gunLayer2;
    bool gunOnCooldown;


    // AnimStates are used by the animation queue - containing,
    // clipName - information on which animation clip to play via Crossfade() - must be in the animator's animation controller
    // duration - how long to play the animation for
    // disableBufferOnStart - whether or not to allow an attack to be buffered immediately.
    // disableCancelOnStart - whether or not we can cancel this move initially.
    public class AnimState {
        public string clipName;
        public float duration;
        public float transitionTime;
        public bool disableBufferOnStart; // Usually false.
        public bool disableCancelOnStart; // Usually true.
        public bool disableMovement;

        public AnimState(string clipName, float duration, float transitionTime, bool disableBufferOnStart, bool disableCancelOnStart, bool disableMovement) {
            this.clipName = clipName;
            this.duration = duration;
            this.transitionTime = transitionTime;
            this.disableBufferOnStart = disableBufferOnStart;
            this.disableCancelOnStart = disableCancelOnStart;
            this.disableMovement = disableMovement;
        }
    }

    public Queue<AnimState> animQueue = new Queue<AnimState>();
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        debugqueuesize = animQueue.Count;
        InputHandler();
        if (!isInAction && animQueue.Count > 0) {
            StartCoroutine(AnimationQueueHandler());
        }
        if (animQueue.Count == 0) {
            if (!atIdle && !isInAction && playerMovement.rb.velocity.magnitude < 0.1f) {
                animQueue.Enqueue(new AnimState("Idle", 0.1f, 0.25f, true, true, true));
                isRunning = false;
            } else if (!isInAction && playerMovement.rb.velocity.magnitude > 0.1f && !isRunning) {
                animator.CrossFade("Run", 0.0f, 0);
                animator.CrossFade("Run", 0.0f, 1);
                animator.CrossFade("Run", 0.0f, 2);
                //animQueue.Enqueue(new AnimState("Run", 0.833f, 0.25f, false, false, false));
                atIdle = false;
                isRunning = true;
            }
        }


        

        if (!isInAction) {
            ResetBufferedAttackState();
        }
    }


    void InputHandler() {
        // Weapon Swap
        if (Input.GetKeyDown(KeyCode.Q)) {
            FullAnimationReset();
            Debug.Log("Swap");
            meleeMode = !meleeMode;
            if (meleeMode) {
                // animator.SetLayerWeight(1, 1);
                // animator.SetLayerWeight(2, 1);
                gunLayer1.SetActive(true);
                gunLayer2.SetActive(false);
            } else {
                //animator.SetLayerWeight(1, 1);
                // animator.SetLayerWeight(1, 0);
                // animator.SetLayerWeight(2, 0);
                animator.CrossFade("Aim", 0.15f, 1);
                gunLayer1.SetActive(false);
                gunLayer2.SetActive(true);
            }
        }
        // if (Input.GetKeyDown(KeyCode.Alpha1)) {
        //     animator.SetLayerWeight(1, 0);
        //     animator.SetLayerWeight(2, 0);
        // }

        if (Input.GetMouseButtonDown(0)) {
            if (meleeMode) {
                MeleeAttackHandler();
            } else {
                if (!gunOnCooldown) {
                    StartCoroutine(ShootGun());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            playerMovement.Dash();
        }
    }

    IEnumerator ShootGun() {
        gunOnCooldown = true;
        animator.CrossFade("Aim", 0.1f, 1);
        yield return null;
        animator.CrossFade("Shoot", 0.05f, 1);
        yield return new WaitForSeconds(0.75f);
        gunOnCooldown = false;
    }

    private void MeleeAttackHandler() {
        // Queue up an attack, if we are not attacking or if we are allowed to buffer.
        // Attack buffer true/false is expected to be handled by the animation clip utilizing Events.
        if (!isInAction || allowBufferInput) {

            if (allowBufferInput) {
                // only accept one buffer at a time.
                allowBufferInput = false;
            }
            Debug.Log(" + {BUFFER} Queuing Swing: " + attackState);
            switch(attackState) {
                case 1:
                    animQueue.Enqueue(new AnimState("Heavy1", 2.708f, 0.0f, false, true, true));
                    break;
                case 2:
                    animQueue.Enqueue(new AnimState("Heavy2", 2.667f, 0f, false, true, true));
                    break;
                case 3:
                    animQueue.Enqueue(new AnimState("Heavy3", 2.667f, 0f, false, true, true));
                    break;
                case 4:
                    animQueue.Enqueue(new AnimState("Heavy4", 2.667f, 0f, false, true, true));
                    break;
                case 5:
                    animQueue.Enqueue(new AnimState("Heavy5", 2.042f, 0f, false, true, true));
                    break;
            }
            attackState++;
        }
    }

    // Try to avoid using.
    public void FullAnimationReset() {
        animQueue.Clear();
        StopAllCoroutines();
        isInAction = false;
        playerMovement.allowMovement = true;
        gunOnCooldown = false;
    }

    public void MovementEvent(float duration) {
        StartCoroutine(playerMovement.MovePlayerForDuration(100, duration));
    }

    public void EnableMovement() {
        playerMovement.allowMovement = true;
    }

    // Plays the first item in the Animation Queue for the specified time of that item. Returns to idle after the animation ends.
    IEnumerator AnimationQueueHandler() {
        atIdle = false;
        isInAction = true;
        isRunning = false;


        AnimState currAnimation = animQueue.Dequeue();

        playerMovement.allowMovement = !currAnimation.disableMovement;

        Debug.Log("[Q] - Playing animation with name: " + currAnimation.clipName);
        animator.CrossFade("Idle", 0.1f, 0);
        animator.CrossFade("Idle", 0.1f, 1);
        animator.CrossFade("Idle", 0.1f, 2);
        yield return null;
        animator.CrossFade(currAnimation.clipName, currAnimation.transitionTime, 0);
        animator.CrossFade(currAnimation.clipName, currAnimation.transitionTime, 1);
        animator.CrossFade(currAnimation.clipName, currAnimation.transitionTime, 2);

        // Handle additional conditions for this AnimState.
        allowBufferInput = !currAnimation.disableBufferOnStart;
        allowAnimationCancel = !currAnimation.disableCancelOnStart;

        if (currAnimation.clipName.CompareTo("Idle") == 0) {
            Debug.Log(" > Returning to idle state.");
            atIdle = true;
        }

        // For the duration that the animation is playing...
        float elapsedTime = 0.0f;
        bool cancelAnimation = false;
        // If cancelAnimation becomes true, for any reason, then we will stop this animation, we will stop this loop.
        while (elapsedTime < currAnimation.duration && !cancelAnimation) {
            // Check if we should cancel the animation, and there is another animation queued.
            // Note that animations are expected to only be in queue when they can be played next,
            // - so basically, a walk animation won't be queued until the player is idle or the animation can be canceled.
            if (allowAnimationCancel && animQueue.Count > 0 || allowAnimationCancel && playerMovement.playerHasInput) {
                Debug.Log(" > cancel animing");
                cancelAnimation = true;
                playerMovement.allowMovement = false;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isInAction = false;
        playerMovement.allowMovement = true;
        Debug.Log("[Q] - Ended QueueHandler.");
    }
    

    // Used by Events in Imported Animation Clips in order to QUEUE UP the next attack, playing once available via the AnimationQueueHandler();
    // View the events on certain FBX animation clips. Animation events cannot use proper bools.
    // input int 0 for false state, 1 for true.
    private void SetAcceptingBufferInput(int stateNum) {
        bool newState;
        if (stateNum == 0) {
            newState = false;
        } else {
            newState = true;
        }

        
        allowBufferInput = newState;
        Debug.Log(" * Set buffer input state to: " + newState);
    }

    // Used by Events in Imported Animation Clips in order to allow the animation to be canceled early.
    // The AnimationQueueHandler(); will then go to the next queued animation.
    // View the events on certain FBX animation clips. Animation events cannot use proper bools.
    // input int 0 for false state, 1 for true.
    private void SetAllowAnimationCancel(int stateNum) {
        bool newState;
        if (stateNum == 0) {
            newState = false;
        } else {
            newState = true;
        }
        allowAnimationCancel = newState;
        Debug.Log(" * Set allow anim cancel input state to: " + newState);
    }

    private void ResetBufferedAttackState() {
        attackState = 1;
    }

    
}

