using UnityEngine;

public class Killzone : MonoBehaviour
{
    public enum KillzoneType { Ravine, River };
    public KillzoneType type;

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
