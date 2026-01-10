using UnityEngine;

public class Structure : Interactable
{
    public override void Use(PlayerInteraction player, Interactable target)
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
