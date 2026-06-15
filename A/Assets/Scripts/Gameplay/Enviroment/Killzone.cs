using UnityEngine;

public class Killzone : MonoBehaviour
{
    private enum KillzoneType { Ravine, River };
    [SerializeField] private KillzoneType type;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case KillzoneType.Ravine:
                    GameManager.Instance.GameOver("Falling");
                    break;
                case KillzoneType.River:
                    GameManager.Instance.GameOver("Drowning");
                    break;
            }
        }
    }
}
