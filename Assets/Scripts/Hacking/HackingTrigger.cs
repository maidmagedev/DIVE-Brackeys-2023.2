using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTrigger : MonoBehaviour
{
    public GameObject gamePrefab;
    public HackingGameSpawner gameSpawner;

    IHackingGame game;
    SphereCollider sphereCollider;

    bool triggered;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (gamePrefab != null )
        {
            game = gamePrefab.GetComponent<IHackingGame>();
        }
        else
        {
            Debug.Log("This interactable doesn't have a game prefab selected.");
        }
        triggered = false;
    }

    void Update()
    {
        checkForInput();
    }

    void checkForInput()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E) && !game.isActivated)   
            {
                gameSpawner.CreateGame(gamePrefab);
            }

            if (Input.GetKeyDown(KeyCode.Escape) && game.isActivated)
            {
                gameSpawner.DestroyGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger - " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Something is staying in the trigger - " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something is staying in the trigger - " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
