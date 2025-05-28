using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{ 
    [SerializeField] private int line;
    [SerializeField] private List<GameObject> cars = new List<GameObject>();
    private float carSpeed;

    [SerializeField] private List<Transform> SpawnPos = new List<Transform>();
    private void OnEnable()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform child in childTransforms)
        {
            if (child.CompareTag("Car"))
            {
                Destroy(child.gameObject);
            }
        }

        GetComponent<Collider>().enabled = true;
        carSpeed = Random.Range(20, 25);
    }
    private void SpawnCar()
    {
        int rand = Random.Range(0, cars.Count);
        GameObject newCar = Instantiate(cars[rand], SpawnPos[line].position,Quaternion.identity);

        newCar.transform.eulerAngles = cars[rand].transform.eulerAngles;

        newCar.transform.parent = transform;
        StartCoroutine(CarTransforming(newCar));
        AudioManager.instance.PlaySFX(AudioManager.instance.carHorn);
       
    }
    private IEnumerator CarTransforming(GameObject _newCar)
    {
        while (_newCar != null)
        {
            _newCar.transform.position -= new Vector3(0, 0, (GameManager.instance.roadManager.currentSpeed + carSpeed) * Time.deltaTime);
            yield return null;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SpawnCar();
            GetComponent<Collider>().enabled = false;
        }
    }
}
