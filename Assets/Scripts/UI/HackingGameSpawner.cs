using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingGameSpawner : MonoBehaviour
{
    private GameObject gamePrefab;
    private IHackingGame currentGame;


    public void Start()
    {
        currentGame = null;
    }

    public void CreateGame(GameObject gameObject)
    {
        gamePrefab = Instantiate(gameObject);
        gamePrefab.transform.parent = this.transform;

        currentGame = gamePrefab.GetComponent<IHackingGame>();
        currentGame.gameSpawner = GetComponent<HackingGameSpawner>();
    }

    public void DestroyGame()
    {
        if (gamePrefab != null)
        {
            Destroy(gamePrefab);
            currentGame = null;
        }
    }
}
