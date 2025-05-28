using UnityEngine;

public class PhaseChange : MonoBehaviour
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

        // if (gameManager.isPhaseThree)
        // {
        //     Debug.Log("Ready for new day — starting Phase One.");
        //     gameManager.ShowSummary();
        // }
        else
        {
            Debug.Log("Not ready for new day — attempting to start Phase Two.");
            gameManager.StartPhaseTwo();
        }
    }
}
