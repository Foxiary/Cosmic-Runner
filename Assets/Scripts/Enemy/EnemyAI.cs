using ReadyPlayerMe.Samples.QuickStart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject energyTankPrefab;

    private float currentTime;
    [SerializeField] private float moveTime;
    [SerializeField] private float speed;

    private void Update()
    {
        if(GameManager.instance.gameState == GameState.Playing)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= moveTime)
            {
                currentTime = 0;
                int nextRoad = Random.Range(0, 3);
                StartCoroutine(Movement(nextRoad));
            }
        }
    }
    private IEnumerator Movement(int nextRoad)
    {
        Vector3 targetPosition = transform.position;

        switch (nextRoad)
        {
            case 0:
                targetPosition = new Vector3(-5, transform.position.y, transform.position.z);
                break;
            case 1:
                targetPosition = new Vector3(0, transform.position.y, transform.position.z);
                break;
            case 2:
                targetPosition = new Vector3(5, transform.position.y, transform.position.z);
                break;
        }

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        SpawnEnergyTank();
    }
    private void SpawnEnergyTank()
    {
        GameObject newTank = Instantiate(energyTankPrefab,transform.position,Quaternion.identity);
    }
}
