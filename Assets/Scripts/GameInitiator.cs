using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Light mainDirectionalLight;
    [SerializeField] private EventSystem mainEventSystem;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private MoneyText moneyText;
    
    // [SerializeField] private IngredientSystem ingredientSystem;
    private async void Start()
    {
        BindObjects();
        // await LoadScene();
    }

    private void BindObjects()
    {
        camera = Instantiate(camera);
        moneyText = camera.GetComponentInChildren<MoneyText>();
        moneyText.Initialize();
        mainDirectionalLight = Instantiate(mainDirectionalLight);
        mainEventSystem = Instantiate(mainEventSystem);
        // ingredientSystem = Instantiate(ingredientSystem);
        loadingCanvas = Instantiate(loadingCanvas);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        
    }

    // private async UniTask LoadScene()
    // {
    //     loadingCanvas.gameObject.SetActive(true);
    //     await UniTask.Delay(3000);
    //     loadingCanvas.gameObject.SetActive(false);
    //     SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    // }
}
