using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    public NavMeshAgent myAgent;
    
    [SerializeField] public Transform target;
    public PathfindingState currentPfState;
    [Header("Settings")]
    public float desiredStoppingDistance = 2.5f;

    public enum PathfindingState {
        following, // keep tracking a target as it moves.  
        movingToDestination, // reach the destination, and stop moving once it has been reached.
        stationary // not currently intending to move anywhere.
    }

    void Update() {
        GoToTarget();
    }

    void SetPathfindingState(PathfindingState state) {
        switch (state) {
            case PathfindingState.following: 
                myAgent.stoppingDistance = desiredStoppingDistance;
                break;
            case PathfindingState.movingToDestination:
                myAgent.stoppingDistance = 0.0f;
                break;
            case PathfindingState.stationary:
                //agent.SetDestination(agent.transform.position);
                break;
        }
        currentPfState = state;
    }

    void GoToTarget() {
        if (target == null) {
            return;
        }
        if (myAgent.isStopped) {
            return;
        }
        if (currentPfState == PathfindingState.stationary) {
            return;
        }
        Vector3 flatPositionAgent = new Vector3(myAgent.transform.position.x, 0f, myAgent.transform.position.z);
        Vector3 flatPositionTarget = new Vector3(target.position.x, 0f, target.position.z);

        // Distance is more lenient on height, but you need to be closer on the flat axis.
        if (Vector3.Distance(flatPositionAgent, flatPositionTarget) < 0.05f && Mathf.Abs(myAgent.transform.position.y - target.position.y) < 0.5f) {
            ConditionReachedDestination();
        }
        myAgent.ResetPath();
        myAgent.SetDestination(target.position);
    }

    void ConditionReachedDestination() {
        if (currentPfState == PathfindingState.movingToDestination) {
            SetPathfindingState(PathfindingState.stationary);

        }
    }

    public void PauseAI(bool pause) {

        if (pause) {
            myAgent.isStopped = true;
        } else {
            myAgent.isStopped = false;
        }
    }
}
