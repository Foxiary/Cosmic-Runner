using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSetting : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Update()
    {
        Material skyboxMaterial = RenderSettings.skybox;

        float currentRotation = skyboxMaterial.GetFloat("_Rotation");
        currentRotation += rotationSpeed * Time.deltaTime;

        if (currentRotation > 360)
        {
            currentRotation -= 360;
        }
        else if (currentRotation < 0)
        {
            currentRotation += 360;
        }

        skyboxMaterial.SetFloat("_Rotation", currentRotation);
    }
}
