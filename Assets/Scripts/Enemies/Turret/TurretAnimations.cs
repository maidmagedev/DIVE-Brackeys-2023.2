using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            animator.CrossFade("Inactive", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            animator.CrossFade("InactiveToActive", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            animator.CrossFade("Active", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            animator.CrossFade("Attack", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            animator.CrossFade("TakeDamage", 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            animator.CrossFade("Death", 0, 0);
        }
    }
}
