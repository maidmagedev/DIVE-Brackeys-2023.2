using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string Name { get; }
    bool IsHacked { get; set; }
    void Interact();    // enter hacking if IsHacked = false
    void Enable();      // enable the interactable being used once/if IsHacked is true
}
