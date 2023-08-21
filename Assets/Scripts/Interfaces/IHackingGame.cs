using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHackingGame
{
    bool isCompleted { get; set; }

    void Activate();
    void Deactivate();
}
