using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; 
    [SerializeField] private GameManager gameManager;

    [Header("Game Time Settings")]
    [SerializeField] private float totalGameSeconds = 420f;
    [SerializeField] private bool isTesting = true; 
    private float currentTime = 0f;
    private int startHour = 8;
    // private int endHour = 17;
    private float totalSimulatedMinutes = 540f; // From 08:00 to 17:00
    private bool isTimerRunning = true;

    public event Action OnTimerEnd;

    void Awake()
    {   
        if (isTesting)
        {
            totalGameSeconds = 60f; // For testing set to 1 minute
            totalSimulatedMinutes = 60f; // Simulate 30 minutes
        }
        else
        {
            totalGameSeconds = 420f; // Default to 7 minutes (420 seconds)
            totalSimulatedMinutes = 540f; // Simulate from 08:00 to 17:00 (9 hours)
        }
        StopTimer();
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timerText.gameObject.SetActive(true); 
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        // timerText.gameObject.SetActive(false);
    }

    public bool IsDayOver => currentTime >= totalGameSeconds;

    void Update()
    {
        if (!isTimerRunning || timerText == null) return;

        currentTime += Time.deltaTime;

        float normalizedTime = Mathf.Clamp01(currentTime / totalGameSeconds);
        float inGameMinutesPassed = normalizedTime * totalSimulatedMinutes;

        int currentHour = startHour + Mathf.FloorToInt(inGameMinutesPassed / 60);
        int CurrentMinute = Mathf.FloorToInt(inGameMinutesPassed % 60);
        //Debug.Log("Current Time: " + currentHour + ":" + CurrentMinute);
        timerText.text = $"{currentHour:00}:{CurrentMinute:00}";

        if (currentTime >= totalGameSeconds)
        {
            isTimerRunning = false;
            timerText.text = "17:00";
            Debug.Log("Game day ended!");
            OnTimerEnd?.Invoke();
            
        }
        
    }
}
