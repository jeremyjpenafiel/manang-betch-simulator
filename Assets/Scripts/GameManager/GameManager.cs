using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Order;
using UnityEngine.SceneManagement;
using NPCSystem;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private NpcSystem npcSystem;


    private void OnEnable()
    {
        timeManager.OnTimerEnd += StartPhaseThree;
    }

    public void SetTimeManager(TimeManager manager)
    {
        timeManager = manager;
    }

    public void SetNpcSystem(NpcSystem system)
    {
        npcSystem = system;
    }

    public void StartPhaseTwo()
    {
        if (OrderCreator.instance == null)
        {
            Debug.LogError("OrderCreator instance not found.");
            return;
        }
        if (OrderCreator.instance.AvailableMealsOnDisplay.Count > 0)
        {
            Debug.Log("Transitioning to Phase Two!");
            timeManager.StartTimer();
            npcSystem.npcSpawner.DoSpawn().Forget();

        }
        else
        {
            Debug.Log("Cannot transition to Phase Two: No meals available on display.");
        }
    }

    public void StartPhaseThree()
    {
        Debug.Log("Transitioning to Phase Three!");
        npcSystem.npcSpawner.PauseSpawn();
        timeManager.StopTimer();

    }

    public void ShowSummary()
    {
        if (!timeManager.IsDayOver)
        {
            Debug.Log("Cannot show summary: Day is not over yet.");
            return;
        }
        if (OrderCreator.instance == null)
        {
            Debug.LogError("OrderCreator instance not found.");
            return;
        }

        if (OrderCreator.instance.AvailableMealsOnDisplay.Count > 0)
        {
            Debug.Log("You must clear the display!");
            Debug.Log(OrderCreator.instance.AvailableMealsOnDisplay.Count + " meals available on display.");
            return;
        }
        else
        {
            Debug.Log("Showing summary of the day!");
            SceneController.Instance?.LoadSummary();
        }
    }



    void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
