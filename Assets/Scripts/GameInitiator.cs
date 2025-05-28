using System;
using Cysharp.Threading.Tasks;
using NPCSystem;
using Order;
using Sirenix.OdinInspector;
using Testing;
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
    [Required, SerializeField] private GameManager gameManager;
    // [Required, SerializeField] private TimeManager timeManager;
    [Required, SerializeField] private OrderChecker orderChecker;
    [Required, SerializeField] private OrderSystem orderSystem;
    [SerializeField] private GameObject testUtils;
    [Required, SerializeField] private GameObject soundManager;
    
    private async void Start()
    {
        BindObjects();
        // await LoadScene();
        Initialize();
    }

    private void BindObjects()
    {

        mainDirectionalLight = Instantiate(mainDirectionalLight, Vector3.zero, Quaternion.identity);
        mainEventSystem = Instantiate(mainEventSystem, Vector3.zero, Quaternion.identity);
        loadingCanvas = Instantiate(loadingCanvas);
        orderCreator = Instantiate(orderCreator);
        orderChecker = Instantiate(orderChecker);
        orderSystem = Instantiate(orderSystem);
        cubPrefab = Instantiate(cubPrefab, Vector3.zero, Quaternion.identity);
        player = Instantiate(player, new Vector3(-9190, 1033, 2221), Quaternion.identity);
        gameManager = Instantiate(gameManager, Vector3.zero, Quaternion.identity);
        // timeManager = Instantiate(timeManager, Vector3.zero, Quaternion.identity);
        npcSystem = Instantiate(npcSystem);
        testUtils = Instantiate(testUtils);
        soundManager = Instantiate(soundManager, Vector3.zero, Quaternion.identity);

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
        {
            gameManager.GetComponent<GameManager>().SetTimeManager(timeManager);
        }
        else
        {
            Debug.LogError("TimeManager not found in scene!");
        }
    }

    private void Initialize()
    {
        npcSystem.SetOrderSystem(orderSystem);
        orderChecker.SetOrderSystem(orderSystem);
    }

    // private async UniTask LoadScene()
    // {
    //     loadingCanvas.gameObject.SetActive(true);
    //     await UniTask.Delay(3000);
    //     loadingCanvas.gameObject.SetActive(false);
    //     SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    // }
}
