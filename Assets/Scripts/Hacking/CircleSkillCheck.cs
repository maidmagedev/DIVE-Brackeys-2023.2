using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSkillCheck : MonoBehaviour, IHackingGame
{
    [SerializeField] HackingGameSpawner hackingGameSpawner;
    
    public bool isCompleted { get; set; }

    public bool isActivated { get; set; }

    public GameObject gamePrefab { get; set; }


    void Start()
    {
        isCompleted = false;
        isActivated = false;

        gamePrefab = Resources.Load<GameObject>("MiniGames/CircleSkillCheckUI");
    }

    public void Activate()
    {
        Debug.Log("Square Skill Check Activated!");
        isActivated = true;
        hackingGameSpawner.CreateGame(gamePrefab);
    }

    public void Finish()
    {
        Debug.Log("Square Skill Check Finished!");
        isCompleted = true;
        hackingGameSpawner.DestroyGame();
    }

    public void Exit()
    {
        Debug.Log("Square Skill Check Exited!");
        isCompleted = false;
        isActivated = false;
        hackingGameSpawner.DestroyGame();
    }
}
