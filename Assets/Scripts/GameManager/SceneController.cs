using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private int currentDay = 0;
    private const int maxDays = 4;

    public int CurrentDay => currentDay;
    public bool IsGameOver => currentDay >= maxDays;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        LoadScene(currentScene);
    }

    public void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        if (currentDay <= maxDays)
        {
            currentDay++;
            Debug.Log($"Loading Day {currentDay}");
            LoadScene("Game");
        } else
        {
            Debug.Log("Game Over! No more days to play.");
            currentDay = 0; // Reset for next game session
            LoadMainMenu();
        }
    }

    public void LoadSummary()
    {
        LoadScene("DaySummary");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
