using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingTrigger : MonoBehaviour
{
    BoxCollider boxCollider;
    IHackingGame game;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    bool checkForInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            game.Activate();
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkForInput();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkForInput();
        }
    }
}
