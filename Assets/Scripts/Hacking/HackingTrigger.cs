using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTrigger : MonoBehaviour, IInteractable
{
    public GameObject gamePrefab;
    public GameObject receiverObj;
    public HackingGameSpawner gameSpawner;
    public bool isHacked;

    private IHackingGame game;
    private IReceiver receiver;
    SphereCollider sphereCollider;

    private bool triggered;

    void Start()
    {
        receiver = receiverObj.GetComponent<IReceiver>();
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
        isHacked = false;
    }

    void Update()
    {
        checkForInput();
    }

    public void checkForInput()
    {
        if (triggered && !isHacked)
        {
            if (Input.GetKeyDown(KeyCode.E) && !game.isActivated)   
            {
                gameSpawner.CreateGame(gamePrefab, this.gameObject);
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

    public void Enable()
    {
        isHacked = true;
        receiver.Activate();
    }
}
