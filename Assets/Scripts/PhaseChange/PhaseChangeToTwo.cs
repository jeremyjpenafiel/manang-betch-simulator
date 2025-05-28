using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangeToTwo : MonoBehaviour
{   
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene!");
        }
    }

    private void OnMouseDown()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager not assigned!");
            return;
        }

        else
        {
            Debug.Log("Not ready for new day — attempting to start Phase Two.");
            gameManager.StartPhaseTwo();
        }
    }
}
