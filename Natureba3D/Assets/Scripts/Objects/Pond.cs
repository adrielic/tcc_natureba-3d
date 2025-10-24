using UnityEngine;
public class Pond : Interactable
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot == null) return;

        if (handSlot is Consumable consumable)
        {
            if (consumable.type == Consumable.ConsumableType.CanteenEmpty)
            {
                consumable.type = Consumable.ConsumableType.CanteenFull;
                consumable.gameObject.name = "Canteen (Full)";
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você encheu a garrafa."));
                Debug.Log("You have filled the canteen.");
            }
            else if (consumable.type == Consumable.ConsumableType.CanteenFull)
            {
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("A garrafa já está cheia."));
                Debug.Log("The canteen is already full.");
            }
        }
    }
}