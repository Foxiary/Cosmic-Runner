using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnManager : SpawnManager
{
    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();

    protected override void Start()
    {
        base.Start();
    }
    public void SpawnNewObstacle(GameObject _road, float distance)
    {
        Dictionary<int, int> obstacleCount = new Dictionary<int, int>();

        for (int line = -5; line <= 5; line += 5)
        {
            int random;
            do
                random = Random.Range(0, obstacleList.Count);
            while (obstacleCount.ContainsKey(random) && obstacleCount[random] >= 2);

            Vector3 spawnPosition = _road.transform.position + new Vector3(line, 0, -distance);

            GameObject newObs = Instantiate(obstacleList[random], spawnPosition, Quaternion.identity);
            newObs.transform.eulerAngles = obstacleList[random].transform.eulerAngles;
            newObs.transform.parent = _road.transform;

            newObs.transform.position = new Vector3(line, newObs.transform.position.y, spawnPosition.z);

            if (!obstacleCount.ContainsKey(random))
                obstacleCount[random] = 0;

            obstacleCount[random]++;
        }
    }
}
