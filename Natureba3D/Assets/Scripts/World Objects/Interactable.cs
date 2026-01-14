using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionText;
    public bool isPickable;

    public virtual void Use(PlayerInteraction player, Interactable target)
    {
        StartCoroutine(GameUIManager.Instance.ShowFeedback("Não é possível usar este item desta forma."));
        Debug.Log("Not implemented.");
    }

    public virtual void OnPickup(Transform playerHands)
    {
        transform.SetParent(playerHands);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (TryGetComponent<Rigidbody>(out var rb))
            rb.isKinematic = true;

        if (TryGetComponent<Collider>(out var col))
            col.enabled = false;

        if (TryGetComponent<Fish>(out var fishAI))
            fishAI.ShutDown();

        // if (this is CarryableOnly && GetComponent<CarryableOnly>().type == CarryableOnly.CarryableType.Beehive)
        // {
        //     GameObject swarm = Instantiate(Resources.Load("Prefabs/Swarm"), transform.position, transform.rotation) as GameObject;
        // }
    }

    public virtual void OnDrop(Vector3 forward)
    {
        transform.SetParent(null);

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.AddForce(forward * 5f, ForceMode.Impulse);
        }

        if (TryGetComponent<Collider>(out var col))
            col.enabled = true;
    }
}
