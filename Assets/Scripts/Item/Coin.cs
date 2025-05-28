using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("rotate info")]
    [SerializeField] private float Yvector;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float delay;

    [Header("transform info")]
    [SerializeField] private float speed;
    private bool canRotate;
    private bool canTransform = false;

    [Header("coin info")]
    public Vector3 initialPosition;
    private void OnEnable()
    {
        canTransform = false;
        canRotate = false;
        Invoke("DelayFor", delay);
    }
    private void Start()
    {
        initialPosition = transform.localPosition;
    }
    private void Update()
    {
        if (canRotate)
        {
            transform.eulerAngles += new Vector3(0, Yvector * Time.deltaTime * rotateSpeed);
        }
    }
    private void DelayFor()
    {
        canRotate = true;
    }
    public void CoinTrigger()
    {
        StartCoroutine("TransformToPlayer");
    }
    public IEnumerator TransformToPlayer()
    {
        canTransform = true;
        canRotate = false;
        if (canTransform)
        {
            do
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position,
                    GameManager.instance.player.transform.position + new Vector3(0, 1.5f, 0),
                    speed * Time.deltaTime);
                yield return null;

            } while (Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) >= 1f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInParent<CoinPool>().AddCoinToPool(this.gameObject);
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySFX(AudioManager.instance.coinCollected);

            //increase coin UI
            GameManager.instance.player.gameObject.GetComponent<PlayerManager>().CollectCoin();
        }
    }
}
