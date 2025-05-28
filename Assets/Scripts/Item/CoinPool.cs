using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public List<GameObject> coinPool;
    private void OnEnable()
    {
        foreach (var coin in coinPool)
        {
            coin.SetActive(true);
            if(coin.transform.localPosition != coin.GetComponent<Coin>().initialPosition)
                coin.transform.localPosition = coin.GetComponent<Coin>().initialPosition;
        }

    }
    public void AddCoinToPool(GameObject coin)
    {
        coinPool.Add(coin);
    }
}
