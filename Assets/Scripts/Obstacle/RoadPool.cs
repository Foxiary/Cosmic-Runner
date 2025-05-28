using System.Collections.Generic;
using UnityEngine;

public class RoadPool : MonoBehaviour
{
    public List<GameObject> roadPool = new List<GameObject>();
    [SerializeField] protected List<GameObject> roadPrefabsList = new List<GameObject>();
    [SerializeField] protected GameObject roadPrefab;

    protected virtual void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        foreach (var prefab in roadPrefabsList)
        {
            GameObject road = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            road.SetActive(false);
            roadPool.Add(road);
        }
    }

    public GameObject GetRoadFromPool(Vector3 spawnPosition)
    {
        if (roadPool.Count == 0)
        {
            return Instantiate(roadPrefab, spawnPosition, Quaternion.identity);
        }

        int randomIndex = Random.Range(0, roadPool.Count);
        GameObject road = roadPool[randomIndex];
        roadPool.RemoveAt(randomIndex);

        road.SetActive(true);
        road.transform.position = spawnPosition;
        return road;
    }

    public void ReturnRoadToPool(GameObject road)
    {
        road.SetActive(false);
        roadPool.Add(road);
    }
}
