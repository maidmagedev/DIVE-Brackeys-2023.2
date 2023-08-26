using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReceiver
{
    public bool isActivated { get; set; }

    public void Activate();
}
