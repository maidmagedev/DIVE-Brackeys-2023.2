using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSkillCheck : MonoBehaviour, IHackingGame
{
    public bool isCompleted { get; set; }

    public bool isActivated { get; set; }

    public HackingGameSpawner gameSpawner { get; set; }


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
        gameSpawner.DestroyGame();
    }

    public void Exit()
    {
        Debug.Log("Square Skill Check Exited!");
        isCompleted = false;
        isActivated = false;
        gameSpawner.DestroyGame();
    }
}
