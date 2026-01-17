using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuration")]
    public Camera playerCamera;
    public float interactionRange = 3f;

    public Transform handsTransform;
    private Interactable itemInHands;

    void Update()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isPaused)
        {
            // Raycast para detectar o alvo
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;
            Interactable target = null;

            if (Physics.Raycast(ray, out hit, interactionRange))
            {
                target = hit.collider.GetComponent<Interactable>();
            }

            // Pegar
            if (Input.GetButtonDown("Grab") && target != null && target.isPickable)
            {
                if (itemInHands == null)
                {
                    GrabItem(target);
                }
                else
                {
                    StartCoroutine(GameUIManager.Instance.ShowFeedback("Suas mãos estão ocupadas."));
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
                consumable.Consume(this);
            }

            // Usar
            if (Input.GetButtonDown("Use"))
            {
                if (itemInHands != null)
                {
                    // Usando um item em outro
                    if (target != null)
                    {
                        target.Use(this, itemInHands);
                    }
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
    }

    void GrabItem(Interactable targetItem)
    {
        if (itemInHands != null)
            return;

        itemInHands = targetItem;
        targetItem.OnPickup(handsTransform);
    }

    void DropItem()
    {
        if (itemInHands == null)
            return;

        Interactable dropped = itemInHands;
        ClearHands();
        dropped.OnDrop(playerCamera.transform.forward);
    }

    public void ClearHands()
    {
        itemInHands = null;
    }

    void OnDrawGizmosSelected()
    {
        // Área de detecção
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + interactionRange));
    }
}
