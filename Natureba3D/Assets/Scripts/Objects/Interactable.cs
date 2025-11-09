using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public string interactionText;
    public bool isPickable;

    public virtual void Use(PlayerInteraction player, Interactable target)
    {
        StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Não é possível usar esse item desta forma."));
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

        if (this is Consumable consumable && consumable.type == Consumable.ConsumableType.FishRaw)
        {
            if (TryGetComponent<NavMeshAgent>(out var agent))
                agent.enabled = false;

            if (TryGetComponent<FishAI>(out var fishAI))
                fishAI.enabled = false;
        }

        if (this is CarryableOnly && GetComponent<CarryableOnly>().type == CarryableOnly.CarryableType.Beehive)
        {
            GameObject swarm = Instantiate(Resources.Load("Prefabs/Swarm"), transform.position, transform.rotation) as GameObject;
        }
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
