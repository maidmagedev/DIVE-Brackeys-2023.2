using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    public float lifeTime; // time before fade, when it starts to die.
    public float fadeTime; // time to fade
    public TextMeshProUGUI text;

    void Start() {
        StartCoroutine(LifeTimer());
    }

    void Update() {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, 1.5f, 0), 1.0f * Time.deltaTime);

    }

    void Die() {
        Destroy(this.gameObject);
    }

    IEnumerator LifeTimer() {
        yield return new WaitForSeconds(lifeTime);
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeTime) {
            float alphaAmount = Mathf.Lerp(1.0f, 0.0f, elapsedTime / fadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alphaAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
        Die();
    }
}
