using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Light mainDirectionalLight;
    [SerializeField] private EventSystem mainEventSystem;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private MoneyText moneyText;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cubPrefab;
    
    // [SerializeField] private IngredientSystem ingredientSystem;
    private async void Start()
    {
        BindObjects();
        // await LoadScene();
    }

    private void BindObjects()
    {
        // camera = Instantiate(camera, Vector3.zero, Quaternion.identity);
        // camera = Instantiate(camera);
        // moneyText = camera.GetComponentInChildren<MoneyText>();
        // moneyText.Initialize();
        mainDirectionalLight = Instantiate(mainDirectionalLight, Vector3.zero, Quaternion.identity);
        mainEventSystem = Instantiate(mainEventSystem, Vector3.zero, Quaternion.identity);
        // ingredientSystem = Instantiate(ingredientSystem);
        loadingCanvas = Instantiate(loadingCanvas);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        // cubPrefab = Instantiate(cubPrefab, Vector3.zero, Quaternion.identity);
        // player at -9757, 15426, 2851
        player = Instantiate(player, new Vector3(-9190, 1033, 2221), Quaternion.identity);
        
    }

    // private async UniTask LoadScene()
    // {
    //     loadingCanvas.gameObject.SetActive(true);
    //     await UniTask.Delay(3000);
    //     loadingCanvas.gameObject.SetActive(false);
    //     SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    // }
}
