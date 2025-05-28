using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : SpawnManager
{
    [SerializeField] private List<Transform> lineCheckPos = new List<Transform>();
    [SerializeField] private float distance;    
    private void OnDrawGizmos()
    {
        for(int i=0;i<lineCheckPos.Count; i++)
            Gizmos.DrawLine(lineCheckPos[i].position, lineCheckPos[i].position + Vector3.back * distance);
    }
}
