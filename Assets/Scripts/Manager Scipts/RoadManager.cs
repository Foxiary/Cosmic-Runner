using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadManager : RoadPool
{
    public List<GameObject> currentRoads = new List<GameObject>();
    public Vector3 lastRoadPosition;
    private float roadLength;
    public float currentSpeed;
    public float speed;
    private ObstacleSpawnManager obstacleManager;

    protected override void Start()
    {
        base.Start();
        obstacleManager = GameObject.Find("SpawnManager").GetComponent<ObstacleSpawnManager>();

        roadLength = roadPrefab.GetComponent<RoadSetting>().meshRenderer.bounds.size.z;
        lastRoadPosition = currentRoads.OrderByDescending(road => road.transform.position.z).First().transform.position;

        foreach (var road in currentRoads.Skip(1))
        {
            obstacleManager.SpawnNewObstacle(road, -20);
            obstacleManager.SpawnNewObstacle(road, 40);
        }
    }

    private void Update()
    {
        RoadTransforming();
    }

    private void RoadTransforming()
    {
        for (int i = currentRoads.Count - 1; i >= 0; i--)
        {
            if (currentRoads[i] == null) continue;

            currentRoads[i].transform.position -= new Vector3(0, 0, currentSpeed * Time.deltaTime);

            if (currentRoads[i].transform.position.z < -roadLength)
            {
                ReturnRoadToPool(currentRoads[i]);
                currentRoads.RemoveAt(i);
            }
        }
    }

    public void SpawnNewRoad()
    {
        lastRoadPosition = currentRoads.OrderByDescending(road => road.transform.position.z).First().transform.position;
        lastRoadPosition += new Vector3(0, 0, roadLength);

        GameObject newRoad = GetRoadFromPool(lastRoadPosition);
        newRoad.transform.eulerAngles = roadPrefab.transform.eulerAngles;
        currentRoads.Add(newRoad);
    }
}
