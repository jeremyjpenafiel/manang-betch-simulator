using System;
using Cysharp.Threading.Tasks;
using NPCSystem;
using Order;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameInitiator : MonoBehaviour
{
    [FormerlySerializedAs("camera")] [Required, SerializeField] private GameObject gameCamera;
    [Required, SerializeField] private Light mainDirectionalLight;
    [Required, SerializeField] private EventSystem mainEventSystem;
    [Required, SerializeField] private Canvas loadingCanvas;
    [Required, SerializeField] private GameObject player;
    [Required, SerializeField] private GameObject cubPrefab;
    [Required, SerializeField] private NpcSystem npcSystem;
    [Required, SerializeField] private OrderCreator orderCreator;
    
    
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
        // SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        cubPrefab = Instantiate(cubPrefab, Vector3.zero, Quaternion.identity);
        // player at -9757, 15426, 2851
        player = Instantiate(player, new Vector3(-9190, 1033, 2221), Quaternion.identity);
        npcSystem = Instantiate(npcSystem);
        orderCreator = Instantiate(orderCreator);
        // _npcSpawner = Instantiate(_npcSpawner, new Vector3(2091,85.017395f,-10304), Quaternion.identity);
        // Instantiate(queuePoint, queuePoint.transform.position, Quaternion.identity);


    }

    // private async UniTask LoadScene()
    // {
    //     loadingCanvas.gameObject.SetActive(true);
    //     await UniTask.Delay(3000);
    //     loadingCanvas.gameObject.SetActive(false);
    //     SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    // }
}
