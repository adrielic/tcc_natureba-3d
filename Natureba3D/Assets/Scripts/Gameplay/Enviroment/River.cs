using UnityEngine;
using System.Collections;

// Classe que gera novos troncos no rio e cria o caminho para eles passarem
public class River : MonoBehaviour
{
    [Header("Trunk Spawning")]
    [SerializeField] private FloatingTrunk trunkPrefab;
    [SerializeField] private Vector2 spawnInterval;

    [Header("Path")]
    [SerializeField] private Color lineColor = Color.cyan;
    [SerializeField] private Color pointColor = Color.blue;
    [SerializeField] private float pointRadius = 0.2f;
    [SerializeField] private bool drawDirectionArrows = true;
    private Transform[] pathWaypoints;

    void Start()
    {
        pathWaypoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            pathWaypoints[i] = transform.GetChild(i);
        }

        StartCoroutine(SpawnLogs()); // Só deve iniciar depois do array ser preenchido
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
            float rSpawn = Random.Range(spawnInterval.x, spawnInterval.y); 

            FloatingTrunk trunk = Instantiate(trunkPrefab, pathWaypoints[0].position, Quaternion.identity); // Instancia os troncos no primeiro waypoint do rio
            trunk.pathPoints = pathWaypoints; // Passa os waypoints do caminho do rio para o tronco

            yield return new WaitForSeconds(rSpawn);
        }
    }

    void OnDrawGizmosSelected()
    {
        Transform[] points = GetComponentsInChildren<Transform>();

        if (points.Length <= 1)
            return;

        for (int i = 1; i < points.Length; i++)
        {
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(points[i].position, pointRadius);

            if (i < points.Length - 1)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(points[i].position, points[i + 1].position);

                if (drawDirectionArrows)
                {
                    DrawArrow(points[i].position, points[i + 1].position);
                }
            }
        }
    }

    void DrawArrow(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;

        float arrowSize = 0.4f;

        Gizmos.DrawLine(to, to + right * arrowSize);
        Gizmos.DrawLine(to, to + left * arrowSize);
    }
}
