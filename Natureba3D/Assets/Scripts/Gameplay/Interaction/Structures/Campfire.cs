using UnityEngine;

public class Campfire : Structure
{
    [SerializeField] private bool isLit = false;
    [SerializeField] private GameObject logs;

    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        if (itemInHands == null) return;

        base.Use(player, itemInHands);

        if (itemInHands is WoodenLog && !isLit)
        {
            isLit = true;
            gameObject.name = "Campfire (Lit)";
            logs.SetActive(true);
        
            Destroy(itemInHands.gameObject);
            player.ClearHands();
        }
        else if (itemInHands is Food fish && !fish.isEdible && isLit)
        {
            fish.Cook();
        }
    }
}
