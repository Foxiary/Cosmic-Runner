using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoosterItem : ItemBooster
{
    [SerializeField] private float jumpHeighBoosted;
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
            itemManager.JumpBooster(jumpHeighBoosted, itemTime);
        }
    }
}
