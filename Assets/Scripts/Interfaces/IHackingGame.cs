using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHackingGame
{
    bool isCompleted { get; set; }
    bool isActivated { get; set; }

    GameObject gamePrefab { get; set; }

    void Activate();
    void Finish();
    void Exit();
}
