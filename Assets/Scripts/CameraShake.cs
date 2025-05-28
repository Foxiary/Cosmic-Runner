using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeIntensity = 1f;
    private float shakeTime = .2f;

    private float timer;
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Start()
    {
        StopShake();
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                StopShake();
        }

    }
    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;

        timer = shakeTime;
    }
    private void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
        timer = 0;

    }
   
}
