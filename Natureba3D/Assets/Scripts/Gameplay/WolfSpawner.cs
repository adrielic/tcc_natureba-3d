using Borodar.FarlandSkies.LowPoly;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField] private bool spawnWolf;
    [SerializeField] private float timeOfDayToSpawn;
    [SerializeField] private GameObject[] wolfObjects;

    void Update()
    {
        if (SkyboxCycleManager.Instance.CycleProgress >= timeOfDayToSpawn)
        {
            foreach (GameObject obj in wolfObjects)
            {
                obj.SetActive(true);
            }
        } 
    }
}
