using UnityEngine;

public class StructureWater : Structure
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot == null)
            return;

        if (handSlot is ConsumableWater canteen)
        {
            if (canteen.fullness == ConsumableWater.CanteenFullness.Empty)
            {
                canteen.fullness = ConsumableWater.CanteenFullness.Full;
                canteen.gameObject.name = "Canteen (Full)";
                StartCoroutine(GameUIManager.Instance.ShowFeedback("Você encheu a garrafa."));
                Debug.Log("You have refilled the canteen.");
            }
            else if (canteen.fullness == ConsumableWater.CanteenFullness.Full)
            {
                StartCoroutine(GameUIManager.Instance.ShowFeedback("A garrafa já está cheia."));
                Debug.Log("The canteen is already full.");
            }
        }
    }
}