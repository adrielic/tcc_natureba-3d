using UnityEngine;

public abstract class Consumable : Interactable
{
    [SerializeField] private bool destroysOnUse;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);

        if (!destroysOnUse) return;

        Destroy(gameObject);
        player.ClearHands();
    }
}
