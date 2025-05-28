using UnityEngine;
using EasyTransition;

public class SceneManagerHandler : MonoBehaviour
{
    private void OnEnable()
    {
        // listen event from Manager
        Debug.Log("Listening event from Manager ");
        GameManager.OnGameSceneLoaded += HandleGameSceneLoaded;
        GameManager.OnNonGameSceneLoaded += HandleNonGameSceneLoaded;

       
    }

    private void OnDisable()
    {
        // unlisten event from Manager when SceneManagerHandler is disable
        GameManager.OnGameSceneLoaded -= HandleGameSceneLoaded;
        GameManager.OnNonGameSceneLoaded -= HandleNonGameSceneLoaded;

    }

    // After loaded
    private void HandleGameSceneLoaded()
    {
        GameManager.instance.SetupToStart();

        // Add more logic to load
        GameManager.instance.TransitionIN(false);
        GameManager.instance.TransitionOUT(false);
    }

    private void HandleNonGameSceneLoaded()
    {
        GameManager.instance.CleanupComponents();
        GameManager.instance.isInitialized = false;
        // Add more logic to load
    }
}
