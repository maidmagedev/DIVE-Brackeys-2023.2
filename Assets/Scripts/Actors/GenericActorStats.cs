using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericActorStats : MonoBehaviour
{
    public String actorName;
    public int maxHealth = 100;
    public int currHealth = 100;
    public bool canTakeDamage = true; // by default is true, but for Invincibility Frames we turn this to false and ignore any TakeDamage calls.

    // Instances created each time TakeDamage() is called.
    public class HitResponse {
        // Defines the relationship between Sender / Reciever relationships for attacks.
        // Damage senders expect a HitResponse to be sent back from the Sender.

        public GenericActorStats reciever; // The recieving end of the hit.
        public bool hitDidDamage; // lets the Sender know if the reciever dodged the hit (or was dead already)
        public bool hitKilledReciever; // lets the Sender know if the reciever died from this interaction.

        public HitResponse(GenericActorStats reciever, bool hitDidDamage, bool hitKilledReciever) {
            this.reciever = reciever;
            this.hitDidDamage = hitDidDamage;
            this.hitKilledReciever = hitKilledReciever;
        }
    }

    public virtual void Die() {
        Debug.Log(actorName + "has died.");
    }

    // The universal generic method that all child classes interact with. Special features (such as anims) should be handled in OnTakeDamage();
    public HitResponse TakeDamage(int damageAmount) {
        if (!canTakeDamage) {
            return new HitResponse(this, false, false);
        }
        OnTakeDamage();
        currHealth -= damageAmount;
        if (currHealth < 0) {
            Die();
            return new HitResponse(this, true, true);
        }
        return new HitResponse(this, true, false);
    }

    // Put unique functionality for each inheritor inside of OnTakeDamage(). Do not override TakeDamage.
    protected abstract void OnTakeDamage();

    // Over a given timeframe, heals health to a desired amount.
    // Not tested.
    public IEnumerator RegenerateHealthOverGivenTime(float totalTime, int healAmount) {
        float elapsedTime = 0.0f;
        int initialHealth = currHealth;
        while (elapsedTime < totalTime && currHealth < maxHealth) {
            currHealth = (int) Mathf.Lerp(initialHealth, maxHealth, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currHealth = initialHealth + healAmount;
    }
}
