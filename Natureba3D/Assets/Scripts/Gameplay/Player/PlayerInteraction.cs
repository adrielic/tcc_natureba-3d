using UnityEngine;
using System;
using System.Collections;
using UnityEditor.Search;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuration")]
    public Camera playerCamera;
    public Animator cameraAnimator;
    [SerializeField][Range(1f, 10f)] private float interactionRange = 3f;
    [SerializeField] private Transform handsTransform;
    [SerializeField] private Animator handAnimator;
    private Interactable itemInHands;

    [Header("VFX")]
    public GameObject vfx;

    void Update()
    {
        if (GameManager.Instance.isPaused) return;

        // Raycast para detectar o alvo
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Interactable target = null;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            target = hit.collider.GetComponent<Interactable>();
        }

        // Pegar
        if (Input.GetButtonDown("Pickup") && target != null && target.isPickable)
        {
            if (itemInHands == null)
            {
                PickupItem(target);
            }
        }

        // Soltar
        if (Input.GetButtonDown("Drop") && itemInHands != null)
        {
            DropItem();
        }

        // Consumir
        if (Input.GetButtonDown("Consume") && itemInHands is Consumable consumable)
        {
            StartCoroutine(
                CallAction("Consume", 0.25f, () =>
                {
                    if (consumable != null)
                        consumable.Consume(this);
                })
            );
        }

        // Usar
        if (Input.GetButtonDown("Use"))
        {
            if (itemInHands != null && target != null)
            {
                // Usando um item em outro
                StartCoroutine(
                    CallAction("Use", 0.25f, () =>
                    {
                        if (target != null && itemInHands != null)
                            target.Use(this, itemInHands);
                    })
                );
            }
            else if (itemInHands == null && target != null)
            {
                // Interação direta sem item na mão
                target.Use(this, null);
            }
        }

        if (target != null)
        {
            Interactable interactable = target.GetComponent<Interactable>();

            if (interactable != null)
            {
                GameUIManager.Instance.ShowInteraction(interactable.interactionText);
            }
        }
        else
        {
            GameUIManager.Instance.ShowInteraction("");
        }
    }

    void PickupItem(Interactable targetItem)
    {
        if (itemInHands != null) return;

        itemInHands = targetItem;
        targetItem.OnPickup(handsTransform);
    }

    void DropItem()
    {
        if (itemInHands == null) return;

        Interactable dropped = itemInHands;
        dropped.OnDrop(playerCamera.transform.forward);

        ClearHands();
    }

    public void ClearHands()
    {
        itemInHands = null;
    }

    IEnumerator CallAction(string triggerName, float delay, Action action)
    {
        handAnimator.SetTrigger(triggerName);

        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }

    public void Hallucinate()
    {
        if (vfx.TryGetComponent<Animator>(out var vfxAnimator))
        {
            vfxAnimator.SetTrigger("Hallucinate");
            Debug.Log("vfx ok");
        }

        if (playerCamera.TryGetComponent<Animator>(out var cameraAnimator))
        {
            cameraAnimator.SetTrigger("Hallucinate");
            Debug.Log("camera ok");
        }
    }

    void OnDrawGizmosSelected()
    {
        // Área de detecção
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + interactionRange));
    }
}
