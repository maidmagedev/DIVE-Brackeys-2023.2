using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHitBox : MonoBehaviour
{
    public int damageAmount;

    void OnTriggerEnter(Collider other) {
        ActorHurtBox recieverHurtBox = other.gameObject.GetComponent<ActorHurtBox>();
        if (recieverHurtBox != null) {
            GenericActorStats.HitResponse hitResponse = recieverHurtBox.actorStats.TakeDamage(damageAmount);
        }
    }
}
