using UnityEngine;
using System.Collections;

public class RiverTrunkSpawner : MonoBehaviour
{
    public FloatingTrunk trunkPrefab;
    public Vector2 spawnInterval = new Vector2(4f, 12f);

    public Transform[] pathPoints;

    void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
            float rSpawn = Random.Range(spawnInterval.x, spawnInterval.y);

            FloatingTrunk log = Instantiate(trunkPrefab, pathPoints[0].position, Quaternion.identity);
            log.pathPoints = pathPoints;

            yield return new WaitForSeconds(rSpawn);
        }
    }
}
