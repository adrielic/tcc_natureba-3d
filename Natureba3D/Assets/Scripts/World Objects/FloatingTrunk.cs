using UnityEngine;

public class FloatingTrunk : MonoBehaviour
{
    [Header("Path")]
    public Transform[] pathPoints;
    public float moveSpeed = 3f;
    public float rotationSpeed = 1f;

    int currentPointIndex = 0;

    [Header("Collision Checking")]
    public Vector3 boxDimensions;
    Transform player;

    void Update()
    {
        if (pathPoints.Length == 0)
            return;

        Transform targetPoint = pathPoints[currentPointIndex];

        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.position, targetPoint.position);

        if (distance < 0.2f)
        {
            currentPointIndex++;

            if (currentPointIndex >= pathPoints.Length)
            {
                Destroy(gameObject);
            }
        }

        bool isPlayerOnTrunk = CheckContact();

        if (player != null && isPlayerOnTrunk)
        {
            if (player.parent != transform)
            {
                player.SetParent(transform);
                Debug.Log("The player is standing on the trunk.");
            }
        }
        else if (player != null && player.parent == transform)
        {
            player.SetParent(null);
            player = null;
            Debug.Log("The player left the trunk.");
        }
    }

    bool CheckContact()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, boxDimensions);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                player = hit.transform;
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        // Área de detecção
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxDimensions);
    }
}
