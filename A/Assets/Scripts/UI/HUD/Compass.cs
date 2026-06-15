using UnityEngine;

public class Compass : MonoBehaviour 
{
    private Transform playerTransform;
    private Vector3 dir;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    void Update()
    {
        dir.z = playerTransform.eulerAngles.y;
        transform.localEulerAngles = dir;
    }
}
