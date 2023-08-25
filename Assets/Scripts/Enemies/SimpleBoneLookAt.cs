using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SimpleBoneLookAt : MonoBehaviour
{
    [SerializeField] Transform lookTarget; // the object to look towards.
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK() {
        animator.SetLookAtWeight(2);
        animator.SetLookAtPosition(lookTarget.position);
    }
}
