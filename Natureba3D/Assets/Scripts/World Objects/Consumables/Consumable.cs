using UnityEngine;

public class Consumable : Interactable
{
    public virtual void Consume(PlayerInteraction player)
    {
        Debug.Log($"Consumed {gameObject.name}");
        Destroy(gameObject);
        player.ClearHands();
    }
}
