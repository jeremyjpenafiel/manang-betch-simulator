using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChange : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void StartPhaseTwo()
    {
        gameManager.StartPhaseTwo();
        Debug.Log("Phase Two Started!");
    }
    

}
