using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBooster : MonoBehaviour
{
    protected ItemManager itemManager;
    protected Player player;
    protected Vector3 startPosition;

    protected float amplitude = 0.25f;
    protected float frequency = 3;

    public float itemTime;
    protected bool isBoosting = false;

    protected virtual void Start()
    {
        startPosition = transform.position;
        itemManager = GameObject.Find("SpawnManager").GetComponent<ItemManager>();
    }
    protected virtual void Update()
    {
        ItemTransforming();
        if(player == null)
            player = GameManager.instance.player;
    }

    private void ItemTransforming()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
