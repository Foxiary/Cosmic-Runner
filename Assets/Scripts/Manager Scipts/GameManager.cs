using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
using System.Collections;
using System;
public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public GameState gameState;

    public static GameManager instance;
    public static event Action OnGameSceneLoaded;
    public static event Action OnNonGameSceneLoaded;



    public RoadManager roadManager;
    public ObstacleSpawnManager roadSpawnManager;
    public GUIManager guiManager;
    public ItemManager itemManager;
    public Player player;

    [Header("Game Logic")]
    public float currentPlayTime;
    public float maxRoundSpeed;

    public bool canStart = false;
    public bool isDeath; // use to call DeathGUI
    private float timeToStart; // setup to start from loading screen
    public bool isInitialized = false;
    private bool gameLoaded = false;
    [Header("Use for load Avatar")]
    private bool networking;

    private bool playerLoaded = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    private void InputHandle()
    {
        if (canStart && Input.GetKeyDown(KeyCode.C) && gameState == GameState.GameOver)
            RestartGame();
        //add more input here
    }

    private void Update()
    {
        if(isInitialized)
        {
            InputHandle();

            if (roadManager != null)
                GameSpeedIncrease();

            if (!canStart)
                timeToStart += Time.unscaledDeltaTime;

            if (CheckingToStart())
                Starting();
        }
    }
    private bool NetworkChecking()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.LogError("No Network Connected !!!");
            return false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork ||
            Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Networking...");
            return true;
        }
        return false;
    }
    #region Player Check
    private void LoadPlayer()
    {
        playerLoaded = false;

        var loader = player.GetComponent<ThirdPersonLoader>();
        if (loader == null)
        {
            Debug.LogError("ThirdPersonLoader component is missing on the player object!");
            return;
        }
        loader.OnLoadComplete += OnPlayerLoadComplete;

        Debug.Log("Player Loading...");
    }
    private void OnPlayerLoadComplete()
    {
        playerLoaded = true;
        Debug.Log("Player Loading Completed");

        var loader = player.GetComponent<ThirdPersonLoader>();
        if (loader != null)
        {
            loader.OnLoadComplete -= OnPlayerLoadComplete;
        }
    }
    public void SetPlayerAnimator(Animator anim)
    {
        player.anim = anim;
    }
    #endregion
    #region Component Check
    public void LoadComponents()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
        itemManager = GameObject.Find("SpawnManager").GetComponent<ItemManager>();
        roadSpawnManager = GameObject.Find("SpawnManager").GetComponent<ObstacleSpawnManager>();
        guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        Debug.Log("Components loaded.");
    }
    public void CleanupComponents()
    {
        roadManager = null;
        itemManager = null;
        roadSpawnManager = null;
        guiManager = null;
        player = null;

        Debug.Log("Components cleaned up.");
    }
    private bool AreComponentsLoaded()
    {
        return (roadManager != null && itemManager != null && roadSpawnManager != null && guiManager != null && player != null
            && player.anim != null);
    }
    private void CheckDependencies()
    {
        if (!AreComponentsLoaded())
        {
            Debug.LogError("Missing Component");
            LoadComponents();
        }
    }
    #endregion
    #region Game State
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void PauseGame()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0;

    }
    public void GameOver()
    {
        //use to call DeatState
        gameState = GameState.GameOver;
        if (isDeath)
            Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1;
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main menu");
        gameState = GameState.MainMenu;
    }
    private void LoadMenuScene()
    {
        SceneManager.LoadScene("Main menu");
    }
    #endregion
    #region Game Logic
    private void GameSpeedIncrease()
    {
        if (gameState == GameState.Playing)
        {
            if (roadManager.currentSpeed < maxRoundSpeed)
            {
                roadManager.currentSpeed += 0.15f * Time.deltaTime;
            }
        }
    }
    #endregion
    #region Scene Check
    public void TransitionIN(bool _)
    {
        if(guiManager != null)
            guiManager.transitionIN.gameObject.SetActive(_);
    }
    public void TransitionOUT(bool _)
    {
        if (guiManager != null)
            guiManager.transitionOUT.gameObject.SetActive(_);
    }
    private IEnumerator LoadAudioAndStart()
    {
        yield return StartCoroutine(AudioManager.instance.LoadAudio());
    }
    public void SetupToStart()
    {
        Debug.Log("Begin loading ...");
        isInitialized = true;
        LoadComponents();

        guiManager.loadScreen.SetActive(true);

        gameState = GameState.Paused;
        LoadPlayer();
        CheckDependencies();
        TransitionOUT(false);

        timeToStart = 0;
        currentPlayTime = 0;

        roadManager.currentSpeed = 0;

        networking = NetworkChecking();
        Debug.Log("Network "+ networking.ToString());
        StartCoroutine(LoadAudioAndStart());



        isDeath = false;
        playerLoaded = false;
        canStart = false;
        gameLoaded = true;
    }
    private bool CheckingToStart()
    {
        if (!canStart &&
                networking &&
                playerLoaded &&
                AreComponentsLoaded() &&
                AudioManager.instance.audioLoaded &&
                timeToStart >= 4)
            return true;
        else if(gameLoaded)
            Debug.LogError("Can't Start !!!");
        return false;
    }
    private void Starting()
    {
        TransitionOUT(true);
        canStart = true;
        roadManager.currentSpeed = roadManager.speed;
        ResumeGame();
    }
    #endregion
    #region Scene Load
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            OnGameSceneLoaded?.Invoke();
            gameLoaded = false;
        }
        else
        {
            OnNonGameSceneLoaded?.Invoke(); 
        }
    }

    #endregion
}

