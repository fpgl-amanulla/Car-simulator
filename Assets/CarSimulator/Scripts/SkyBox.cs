using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    private static readonly int RotationId = Shader.PropertyToID("_Rotation");

    private void Update () {
        RenderSettings.skybox.SetFloat(RotationId, Time.time * rotationSpeed);
    }
}
