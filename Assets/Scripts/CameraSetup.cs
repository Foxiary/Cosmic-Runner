using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CameraSetup : MonoBehaviour
{
    private GameObject player;
    private CinemachineVirtualCamera cinemachineCamera;


    private void Start()
    {
        player = GameManager.instance.player.gameObject;
        cinemachineCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

        StartFollowingPlayer();
    }

    void StartFollowingPlayer()
    {
        cinemachineCamera.transform.eulerAngles = new Vector3(10,0,0);
        cinemachineCamera.Follow = player.transform;
    }
}
