using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetpackBootster : ItemBooster
{
    public float flyHeight;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            player.jetpackBooster = this;
            gameObject.SetActive(false);
            itemManager.JetpackBooster(itemTime);
        }
    }
}
