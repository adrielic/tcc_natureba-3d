using Borodar.FarlandSkies.LowPoly;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] float fullRotationTime = 120f;

    void Update()
    {
        if (fullRotationTime <= 0f)
                return;

            float degreesPerSecond = 360f / fullRotationTime;

            SkyboxController.Instance.CloudsRotation += degreesPerSecond * Time.deltaTime;

            // Mantém sempre entre 0 e 360
            SkyboxController.Instance.CloudsRotation %= 360f;
    }
}
