using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;

    public void SetTimeManager(TimeManager tm)
    {
        timeManager = tm;
    }

    public void StartPhaseOne()
    {
        Debug.Log("Phase One Started!");
        timeManager.ResetTimer();
        timeManager.PauseTimer();
        timeManager.SetTimerUI(null); // Assuming you want to hide the timer UI at the start of phase one
    }

    public void StartPhaseTwo()
    {
        Debug.Log("Phase Two Started!");
        timeManager.ResumeTimer();
    }
    public void StartPhaseThree()
    {
        Debug.Log("Phase Three Started!");
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        timeManager.PauseTimer();
    }

    public bool IsGameOver() => false;
}
