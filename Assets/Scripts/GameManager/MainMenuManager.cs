using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // private static GameManager instance;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject letterCanvas;
    void Start()
    {
        menuCanvas.gameObject.SetActive(true);
        letterCanvas.gameObject.SetActive(false);
    }
    public void Play()
    {
        menuCanvas.gameObject.SetActive(false);
        letterCanvas.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneController.Instance.LoadGame();
    }

    public void showHowToPlay()
    {

        // Logic to show the instructions 
    }

    public void QuitGame()
    {
        Application.Quit(); // idk wala gagana
    }
}
