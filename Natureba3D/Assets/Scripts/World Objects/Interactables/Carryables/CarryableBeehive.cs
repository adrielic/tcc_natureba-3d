using UnityEngine;

public class CarryableBeehive : Carryable
{
    public GameObject swarmPrefab;
    bool swarmSpawned = false;

    public override void OnPickup(Transform playerHands)
    {
        base.OnPickup(playerHands);
        
        if (!swarmSpawned)
        {
            Instantiate(swarmPrefab, transform.position, transform.rotation);
            swarmSpawned = true;
        }
    }
}