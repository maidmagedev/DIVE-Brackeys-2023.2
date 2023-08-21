using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSkillCheck : MonoBehaviour, IHackingGame
{
    public bool isCompleted { get; set; }

    public bool isActivated { get; set; }


    void Start()
    {
        isCompleted = false;
        isActivated = false;
    }

    public void Activate()
    {
        Debug.Log("Square Skill Check Activated!");
        isActivated = true;
    }

    public void Finish()
    {
        Debug.Log("Square Skill Check Finished!");
        isCompleted = true;
    }

    public void Exit()
    {
        Debug.Log("Square Skill Check Exited!");
        isCompleted = false;
        isActivated = false;
    }
}
