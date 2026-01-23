using UnityEngine;

public abstract class Carryable : Interactable
{
    public override void OnPickup(Transform playerHands)
    {
        base.OnPickup(playerHands);
    }

    public override void OnDrop(Vector3 forward)
    {
        base.OnDrop(forward);
    }
}