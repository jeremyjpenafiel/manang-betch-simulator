using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Light mainDirectionalLight;
    [SerializeField] private EventSystem mainEventSystem;
    [SerializeField] private Canvas loadingCanvas;
    
    [SerializeField] private IngredientSystem ingredientSystem;
    private async void Start()
    {
        BindObjects();
    }

    private void BindObjects()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        camera = Instantiate(camera);
        mainDirectionalLight = Instantiate(mainDirectionalLight);
        mainEventSystem = Instantiate(mainEventSystem);
        ingredientSystem = Instantiate(ingredientSystem);
        loadingCanvas = Instantiate(loadingCanvas);
    }
}
