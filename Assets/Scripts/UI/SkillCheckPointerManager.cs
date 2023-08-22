using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkillCheckPointerManager : MonoBehaviour
{
    [SerializeField] SkillCheckMarker marker;
    public float rotationSpeed;

    private RectTransform rectTransform;

    private float targetRot;

    [SerializeField] private float currentRot;
    private float rotationDir;
    private bool isActive;

    private int completedCount;

    void Start()
    {
        marker = transform.parent.GetComponentInChildren<SkillCheckMarker>();
        rectTransform = GetComponent<RectTransform>();

        currentRot = Random.Range(0.0f, 360.0f);
        targetRot = marker.RandomRot();

        isActive = true;
        rotationSpeed = 300.0f;
        rotationDir = 1.0f;

        completedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRot * rotationDir));
            currentRot += rotationSpeed * Time.deltaTime;

            if (currentRot > 360.0f)
            {
                currentRot -= 360.0f;
            } else if (currentRot < 0.0f) {
                currentRot += 360.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                float diff = targetRot - Mathf.Abs(currentRot);
                if (Mathf.Abs(diff) < 15.0f)
                {
                    targetRot = marker.RandomRot();
                    completedCount++;
                }

                rotationSpeed *= -1.0f;
            }
        }
    }
}
