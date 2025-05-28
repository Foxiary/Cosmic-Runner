using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject transitionIN;
    public GameObject transitionOUT;

    public GameObject touchArea;
    private void Start()
    {
        //transitionOUT.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || CanTouch())
        {
            Time.timeScale = 1;
            transitionIN.gameObject.SetActive(true);
            AudioManager.instance.PlaySFX(AudioManager.instance.brush);
            Invoke("LoadScene", 1f);
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }
    private bool CanTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (RectTransformUtility.RectangleContainsScreenPoint(touchArea.GetComponent<RectTransform>(), touch.position, null))
                return true;
        }
        return false;
    }
}