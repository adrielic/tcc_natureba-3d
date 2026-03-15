using Borodar.FarlandSkies.LowPoly;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField] private bool spawnWolf;
    [SerializeField] private float timeOfDayToSpawn;
    [SerializeField] private float timeOfDayToDespawn;
    [SerializeField] private GameObject[] wolfObjects;
    [SerializeField] private GameObject sfxObject;

    void Update()
    {
        if (!spawnWolf) return;

        float cycle = SkyboxCycleManager.Instance.CycleProgress;

        if (cycle >= timeOfDayToSpawn)
        {
            foreach (GameObject obj in wolfObjects)
            {
                obj.SetActive(true);
                sfxObject.SetActive(true);
            }
        }
        else if (cycle >= timeOfDayToDespawn)
        {
            foreach (GameObject obj in wolfObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
