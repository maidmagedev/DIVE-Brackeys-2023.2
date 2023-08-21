using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTrigger : MonoBehaviour
{
    SphereCollider sphereCollider;
    IHackingGame game;

    bool triggered;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        game = GetComponent<IHackingGame>();
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
                game.Activate();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && game.isActivated)
            {
                game.Exit();
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
