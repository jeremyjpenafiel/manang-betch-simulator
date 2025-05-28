using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float totalGameSeconds = 420f; // 7 minutes in seconds
    private float currentTime = 0f;
    private int startHour = 8;
    private int endHour = 17;
    private float totalSimulatedMinutes = 540f; // From 08:00 to 17:00
    private bool isTimerRunning = true;

    public void SetTimerUI(TextMeshProUGUI timer)
    {
        timerText = timer;
    }

    public void SetGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    void Start()
    {
        timerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isTimerRunning || timerText == null) return;

        currentTime += Time.deltaTime;

        float normalizedTime = Mathf.Clamp01(currentTime / totalGameSeconds);
        float inGameMinutesPassed = normalizedTime * totalSimulatedMinutes;

        int currentHour = startHour + Mathf.FloorToInt(inGameMinutesPassed / 60);
        int CurrentMinute = Mathf.FloorToInt(inGameMinutesPassed % 60);
//        Debug.Log("Current Time: " + currentHour + ":" + CurrentMinute);
        timerText.text = $"{currentHour:00}:{CurrentMinute:00}";

        if (currentTime >= totalGameSeconds)
        {
            isTimerRunning = false;
            timerText.text = "17:00";
            Debug.Log("Game day ended!");
            gameManager?.GameOver();
        }
    }

    public void ResetTimer() => currentTime = 0f;
    public void PauseTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer paused at: " + timerText.text);
        timerText.gameObject.SetActive(false);
    }
    public void ResumeTimer()
    {
        isTimerRunning = true;
        timerText.gameObject.SetActive(true);
        Debug.Log("Timer resumed at: " + timerText.text);
    }
}
