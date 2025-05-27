using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas letterCanvas;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetTimeManager(TimeManager tm)
    {
        timeManager = tm;
    }

    public void Play()
    {
        menuCanvas.gameObject.SetActive(false);
        letterCanvas.gameObject.SetActive(true);
    }

    public void StartPhaseOne()
    {
        Debug.Log("Phase One Started!");
        timeManager.ResetTimer();
        timeManager.PauseTimer();
        SceneManager.LoadScene("Game");
        // timeManager.SetTimerUI(null); // Assuming you want to hide the timer UI at the start of phase one

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

    // public void GameOver()
    // {
    //     Debug.Log("Game Over. Returning to Main Menu...");
    //     SceneManager.LoadScene("MainMenu");
    // }

}
