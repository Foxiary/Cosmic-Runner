using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel;

    public Slider musicSlider;
    public Slider sfxSlider;
    
    private bool isPaused;
    public void Debuging()
    {
        Debug.Log("btn");
    }

    private void Update()
    {
        HandleInput();
        SetVolumn();
    }
    private void HandleInput()
    {
        //PausePanel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }
    public void PauseGame()
    {
        GameManager.instance.PauseGame();
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
        pausePanel.SetActive(false);
    }
    public void QuitGame()
    {
        GameManager.instance.QuitToMenu();
    }
    
    private void SetVolumn()
    {
        AudioManager.instance.musicSource.volume = musicSlider.value;
        AudioManager.instance.SFXSource.volume = sfxSlider.value;
    }
    
}
