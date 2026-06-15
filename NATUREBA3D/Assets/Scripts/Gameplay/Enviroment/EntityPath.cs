using UnityEngine;

public class EntityPath : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private Color lineColor = Color.grey;
    [SerializeField] private Color pointColor = Color.yellow;
    [SerializeField] private float pointRadius = 0.2f;
    [SerializeField] private bool drawDirectionArrows = true;
    private Transform[] pathWaypoints;

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
