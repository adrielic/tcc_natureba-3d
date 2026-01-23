using UnityEngine;

public class WaterSource : Structure
{
    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        if (itemInHands == null) return;

        if (itemInHands is Water canteen)
        {
            if (canteen.fullness == Water.CanteenFullness.Empty)
            {
                canteen.SwitchState(Water.CanteenFullness.Full);
                
                Debug.Log("The player refilled the canteen.");
            }
            else
            {
                StartCoroutine(GameUIManager.Instance.ShowNotification("A garrafa já está cheia."));

                Debug.Log("Canteen already full.");
            }
        }
    }
}