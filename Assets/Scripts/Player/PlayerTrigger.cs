using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Car")
        {
            gameObject.SetActive(false);
            GameManager.instance.roadManager.currentSpeed = 0;
            GameManager.instance.GameOver();
        }
    }
}
