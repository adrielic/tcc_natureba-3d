using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionText;
    public bool isPickable;
    [SerializeField] protected Renderer meshRenderer;

    public virtual void Consume(PlayerInteraction player)
    {
        Debug.Log($"Consumed {gameObject.name}");
    }

    public virtual void Use(PlayerInteraction player, Interactable itemInHands)
    {

    }

    public virtual void OnPickup(Transform playerHands)
    {
        transform.SetParent(playerHands);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = true;
        }

        if (TryGetComponent<Collider>(out var collider))
        {
            collider.enabled = false;
        }

        if (TryGetComponent<Entity>(out var fish))
        {
            fish.ShutDown();
        }

        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }

    public virtual void OnDrop(Vector3 forward)
    {
        transform.SetParent(null);

        if (TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(forward * 5f, ForceMode.Impulse);
        }

        if (TryGetComponent<Collider>(out var collider))
        {
            collider.enabled = true;
        }

        if (TryGetComponent<Bait>(out var bait))
        {
            bait.isEnabled = true;
        }

        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

    public void DisableInteraction()
    {
        isPickable = false;
        interactionText = "";
    }
}
