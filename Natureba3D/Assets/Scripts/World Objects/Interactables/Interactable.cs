using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactionText;
    public bool isPickable;

    public virtual void Use(PlayerInteraction player, Interactable handSlot)
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
        {
            rb.isKinematic = true;
        }

        if (TryGetComponent<Collider>(out var col))
        {
            col.enabled = false;
        }

        if (TryGetComponent<Fish>(out var fishAI))
        {
            fishAI.ShutDown();
        }

        if (TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
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
        {
            col.enabled = true;
        }

        if (TryGetComponent<Bait>(out var bait))
        {
            bait.isEnabled = true;
        }

        if (TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
