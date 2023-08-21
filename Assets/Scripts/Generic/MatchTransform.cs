using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Updates this transform.position to the same position as the target transform inside of Update();
public class MatchTransform : MonoBehaviour
{
    [SerializeField] Transform target; 
    public bool matchPosition;
    public bool matchRotation;

    // Update is called once per frame
    void Update()
    {
        if (matchPosition) {
            transform.position = target.position;
        }
        if (matchRotation) {
            transform.rotation = target.rotation;
        }
    }
}
