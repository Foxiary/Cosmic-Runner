using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string currentAvatar;
    public float currentEnergy;

    [Header("Saveable Item")]
    public int currentCoin;
    public int allCoin;

    [Header("Game Stats")]
    public float currentScore;       
    public float bestScore;

    private void InitializePlayer()
    {
        currentEnergy = 0;
        currentCoin = 0;
        currentScore = 0;
    }
    private void Start()
    {
        InitializePlayer();
    }
    private void Update()
    {
        ScoreManager();
        UpdateGUI();
        if (GameManager.instance.isDeath)
            CoinUpdate();
    }
    private void UpdateGUI()
    {
        GUIManager gui = GameManager.instance.guiManager;
        if (gui != null)
        {
            gui.currentScore.text = currentScore.ToString();
            gui.currentCoin.text = currentCoin.ToString();
        }
    }

    private void ScoreManager()
    {
        if (GameManager.instance.gameState == GameState.Playing)
        {
            currentScore += (int)(GameManager.instance.roadManager.currentSpeed * .2f);
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                SaveBestScore();
            }
        }
    }
    public void CoinUpdate()
    {
        allCoin += currentCoin;
        SaveCoins();
    }
    public void CollectCoin()
    {
        currentCoin += 1;
    }
    public void SaveCoins()
    {
        PlayerPrefs.SetInt("AllCoin", allCoin);
        PlayerPrefs.Save();
    }
    public void SaveBestScore()
    {
        PlayerPrefs.SetFloat("BestScore", bestScore); 
        PlayerPrefs.Save();
    }
}
