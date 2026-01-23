using Borodar.FarlandSkies.LowPoly;
using UnityEngine;

// Classe para controle visual das nuvens
public class Clouds : MonoBehaviour
{
    [SerializeField] private float fullRotationTime = 120f; // Duração de uma volta completa das nuvens

    void Update()
    {
        if (fullRotationTime <= 0f) return; 

        float degreesPerSecond = 360f / fullRotationTime; // 360 / 120 = 3

        SkyboxController.Instance.CloudsRotation += degreesPerSecond * Time.deltaTime; // 3/s
        SkyboxController.Instance.CloudsRotation %= 360f;
    }
}
