using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTank : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private bool canFall = true;
    private void Update()
    {
        transform.eulerAngles += new Vector3(0, rotateSpeed*4 * Time.deltaTime);

        if (canFall)
            transform.position -= new Vector3(0, Time.deltaTime * rotateSpeed/5,0);
        else
            transform.position -= new Vector3(0, 0, GameManager.instance.roadManager.currentSpeed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestroyTrigger")
            Destroy(gameObject);

        if (other.gameObject.tag == "Coin")
            other.gameObject.SetActive(false);

        if (other.gameObject.tag == "Player")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.energyCollected);

            Destroy(gameObject);
            GameManager.instance.player.GetComponent<PlayerManager>().currentEnergy += 20;
        }
        

        if (other.gameObject.name == "Plane" || other.gameObject.tag == "Wall")
            canFall = false;
    }

}
