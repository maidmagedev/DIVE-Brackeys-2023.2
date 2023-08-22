using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckMarker : MonoBehaviour
{
    private RectTransform rectTransform;

    private float currentRot;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public float RandomRot()
    {
        currentRot = Random.Range(0.0f, 360.0f);
        rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, currentRot));

        return currentRot;
    }
}
