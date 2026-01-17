using UnityEngine;

public class Structure : Interactable
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
