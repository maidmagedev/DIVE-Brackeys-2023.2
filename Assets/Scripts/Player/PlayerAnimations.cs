using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Mainly a test script, to be replaced or revamped once systems are in place
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    public int attackState = 1;
    public bool atIdle = true;
    public bool isInAction = false;
    private bool allowBufferInput = false; // if true, allows the player to queue up the next move in an attack string.
    public bool allowAnimationCancel = false; // allows this animation to be overridden.
            public int debugqueuesize;


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

        public AnimState(string clipName, float duration, float transitionTime, bool disableBufferOnStart, bool disableCancelOnStart) {
            this.clipName = clipName;
            this.duration = duration;
            this.transitionTime = transitionTime;
            this.disableBufferOnStart = disableBufferOnStart;
            this.disableCancelOnStart = disableCancelOnStart;
        }
    }

    Queue<AnimState> animQueue = new Queue<AnimState>();

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
        if (animQueue.Count == 0 && !atIdle) {
            animQueue.Enqueue(new AnimState("Idle", 0.1f, 0.25f, false, false));
        }

        if (atIdle) {
            attackState = 1;
        }
    }


    void InputHandler() {
        // Concerning the basic attack combo string...
        if (Input.GetMouseButtonDown(0)) {
            // Queue up an attack, if we are not attacking or if we are allowed to buffer.
            // Attack buffer true/false is expected to be handled by the animation clip utilizing Events.
            if (!isInAction || allowBufferInput) {

                if (allowBufferInput) {
                    // only accept one buffer at a time.
                    allowBufferInput = false;
                }
                Debug.Log("Queuing Swing: " + attackState);
                switch(attackState) {
                    case 1:
                        animQueue.Enqueue(new AnimState("Heavy1", 1.042f, 0.1f, false, true));
                        break;
                    case 2:
                        animQueue.Enqueue(new AnimState("Heavy2", 1.333f, 0.25f, false, true));
                        break;
                    case 3:
                        animQueue.Enqueue(new AnimState("Heavy5", 1.792f, 0.25f, false, true));
                        break;
                }
                attackState++;
            }
        }
    }

    // Plays the first item in the Animation Queue for the specified time of that item. Returns to idle after the animation ends.
    IEnumerator AnimationQueueHandler() {
        atIdle = false;
        isInAction = true;
        AnimState currAnimation = animQueue.Dequeue();
        Debug.Log("Playing animation with name: " + currAnimation.clipName);
        animator.CrossFade(currAnimation.clipName, currAnimation.transitionTime, 0);

        // Handle additional conditions for this AnimState.
        allowBufferInput = !currAnimation.disableBufferOnStart;
        allowAnimationCancel = !currAnimation.disableCancelOnStart;

        if (currAnimation.clipName == "Idle") {
            Debug.Log("Returning to idle state.");
            atIdle = true;
        }

        // For the duration that the animation is playing...
        float elapsedTime = 0.0f;
        bool cancelAnimation = false;
        // If cancelAnimation becomes true, for any reason, then we will stop this animation, we will stop this loop.
        while (elapsedTime < currAnimation.duration || (elapsedTime < currAnimation.duration && !cancelAnimation)) {
            // Check if we should cancel the animation, and there is another animation queued.
            // Note that animations are expected to only be in queue when they can be played next,
            // - so basically, a walk animation won't be queued until the player is idle or the animation can be canceled.
            if (allowAnimationCancel && animQueue.Count > 0) {
                cancelAnimation = true;
            }
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        isInAction = false;
        Debug.Log("Ended QueueHandler.");
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
        Debug.Log("Set buffer input state to: " + newState);
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
        allowBufferInput = newState;
        Debug.Log("Set allow anim cancel input state to: " + newState);
    }
}

