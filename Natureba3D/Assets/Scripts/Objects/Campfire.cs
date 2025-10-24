using UnityEngine;

public class Campfire : Interactable
{
    public bool isLit = false;

    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot == null) return;

        // Lenha acende a fogueira
        if (handSlot is CarryableOnly carryable && carryable.type == CarryableOnly.CarryableType.WoodenLog && !isLit)
        {
            isLit = true;
            gameObject.name = "Campfire (Lit)";
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você acendeu a fogueira."));
            Debug.Log("You have lit the campfire.");
            Destroy(handSlot.gameObject);
            player.ClearHands();
        }
        // Peixe cru assa se a fogueira estiver acesa
        else if (handSlot is Consumable consumable && consumable.type == Consumable.ConsumableType.FishRaw && isLit)
        {
            consumable.type = Consumable.ConsumableType.FishCooked;
            consumable.gameObject.name = "Fish (Cooked)";
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você assou o peixe."));
            Debug.Log("You cooked the fish.");
        }
    }
}
