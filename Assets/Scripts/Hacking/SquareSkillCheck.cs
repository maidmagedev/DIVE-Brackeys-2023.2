using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSkillCheck : IHackingGame
{
    public bool isCompleted {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
