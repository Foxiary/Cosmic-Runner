using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.GetComponent<Coin>().CoinTrigger();
        }
    }
}
