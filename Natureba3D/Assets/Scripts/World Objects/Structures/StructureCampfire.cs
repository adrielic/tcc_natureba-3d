using UnityEngine;

public class StructureCampfire : Structure
{
    public bool isLit = false;
    public GameObject logs;

    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot == null)
            return;

        base.Use(player, handSlot);

        // Lenha acende a fogueira
        if (handSlot is CarryableWoodenLog && !isLit)
        {
            isLit = true;
            gameObject.name = "Campfire (Lit)";
            logs.SetActive(true);
            StartCoroutine(GameUIManager.Instance.ShowFeedback("Você acendeu a fogueira."));
            Destroy(handSlot.gameObject);
            player.ClearHands();
        }
        // Peixe cru assa se a fogueira estiver acesa
        else if (handSlot is ConsumableFood fish && !fish.isEdible && isLit)
        {
            fish.isEdible = true;
            fish.gameObject.name = "Fish (Cooked)";
            StartCoroutine(GameUIManager.Instance.ShowFeedback("Você assou o peixe."));
        }
    }
}
