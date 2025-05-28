using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBootster : ItemBooster
{
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
            gameObject.SetActive(false);
            itemManager.MagnetBooster(itemTime);
        }
    }
}
