using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour, IInteractable
{

    private bool triggered;

    void Start()
    {
        triggered = false;
    }

    void Update()
    {
        checkForInput();
    }

    public void checkForInput()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Enable();
            }
        }
    }

    public void Enable()
    {
        
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
