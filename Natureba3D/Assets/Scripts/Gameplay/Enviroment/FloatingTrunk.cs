using UnityEngine;

public class FloatingTrunk : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 1f;

    [HideInInspector] public Transform[] pathPoints;
    private int currentPointIndex = 0;
    Vector3 direction;

    void Update()
    {
        if (pathPoints.Length == 0) return;

        Transform targetPoint = pathPoints[currentPointIndex];

        direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Rotate();
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
    }

    void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);

            Debug.Log("The player is standing on the trunk.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            
            Debug.Log("The player left the trunk.");
        }
    }
}
