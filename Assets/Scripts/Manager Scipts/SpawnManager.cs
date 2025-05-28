using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected virtual void Start()
    {
        gameManager = GameManager.instance;
    }
    
}
