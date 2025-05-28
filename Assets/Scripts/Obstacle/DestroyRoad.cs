using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRoad : MonoBehaviour
{
    RoadManager roadManager;
    private void Awake()
    {
        roadManager = GameObject.Find("RoadManager").GetComponent<RoadManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DestroyTrigger")
        {
            roadManager.currentRoads.Remove(gameObject);
            roadManager.ReturnRoadToPool(gameObject);
            roadManager.SpawnNewRoad();
        }
    }
}
