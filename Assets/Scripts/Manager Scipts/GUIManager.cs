using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public PlayerManager playerStats;

    public GameObject deadUI;
    public GameObject loadScreen;
    [Header("Transition UI")]
    public GameObject transitionIN;
    public GameObject transitionOUT;
    [Header("Energy Bar")]
    public Slider energyBar;
    [Header("Stats")]
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI currentCoin;
    private void Update()
    {
        //deadUI
        if (deadUI.activeSelf != GameManager.instance.isDeath)
            deadUI.SetActive(GameManager.instance.isDeath);

        //loadingScreen
        if (loadScreen.activeSelf && GameManager.instance.gameState == GameState.Playing)
            loadScreen.SetActive(false);

        //update energy
        energyBar.value = playerStats.currentEnergy;
    }

    
}
