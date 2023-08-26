using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IReceiver
{
    public float openSpeed;

    private float currentY;
    private float startY;
    private float targetY;
    
    public bool isActivated { get; set; }

    void Start()
    {
        startY = transform.position.y;

        currentY = startY;
        targetY = startY - 5.0f;
    }

    public void Activate()
    {
        if (!isActivated)
        {
            StartCoroutine(OpenDoor());
            isActivated = true;
        }
    }

    public IEnumerator OpenDoor()
    {
        float elapsedTime = 0.0f;
        float targetTime = 5.0f;
        while (elapsedTime < targetTime)
        {
            currentY = Mathf.Lerp(startY, targetY, elapsedTime / targetTime);
            transform.position = new Vector3(transform.position.x, currentY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
}
