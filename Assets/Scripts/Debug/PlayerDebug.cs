using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Callbacks;

public class PlayerDebug : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAnimations playerAnimations;
    [SerializeField] TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();        
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = "debug values\n";
        textMesh.text += "vel: " + playerMovement.rb.velocity;
        textMesh.text += "\n";
        textMesh.text += "dashes: " + playerMovement.dashStack;
        textMesh.text += "\n";
    }
}
