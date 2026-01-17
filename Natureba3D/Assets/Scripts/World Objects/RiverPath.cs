using UnityEngine;

public class RiverPath : MonoBehaviour
{
    [Header("Gizmos Settings")]
    public Color lineColor = Color.cyan;
    public Color pointColor = Color.blue;
    public float pointRadius = 0.2f;
    public bool drawDirectionArrows = true;

    void OnDrawGizmos()
    {
        Transform[] points = GetComponentsInChildren<Transform>();

        if (points.Length <= 1)
            return;

        for (int i = 1; i < points.Length; i++)
        {
            // Desenha o ponto
            Gizmos.color = pointColor;
            Gizmos.DrawSphere(points[i].position, pointRadius);

            // Desenha a linha até o próximo ponto
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
        Vector3 left  = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;

        float arrowSize = 0.4f;

        Gizmos.DrawLine(to, to + right * arrowSize);
        Gizmos.DrawLine(to, to + left * arrowSize);
    }
}
