using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameSpawner : MonoBehaviour
{
    private GameObject currentGame;


    public void Start()
    {
        currentGame = null;
    }

    public void CreateGame(GameObject gameObject)
    {
        currentGame = Instantiate(gameObject);
        currentGame.transform.parent = this.transform;
        //currentGame.GetComponentInChildren<SkillCheckPointerManager>().game = 
    }

    public void DestroyGame()
    {
        if (currentGame != null)
        {
            Destroy(currentGame);
        }
    }
}
