using UnityEngine;

public abstract class Structure : Interactable
{
    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        Debug.Log($"The player used {itemInHands.gameObject.name} in the {gameObject.name}.");
    }
}
