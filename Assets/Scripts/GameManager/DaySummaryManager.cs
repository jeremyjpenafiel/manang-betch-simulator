using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySummaryManager : MonoBehaviour
{   
    void Start()
    {
        // shouw cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ContinueToNextDay()
    {
        Debug.Log("Continuing to the next day...");
        SceneController.Instance?.LoadGame();
    }
}
