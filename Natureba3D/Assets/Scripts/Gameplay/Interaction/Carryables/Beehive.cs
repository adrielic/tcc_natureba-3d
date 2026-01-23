using UnityEngine;

public class Beehive : Carryable
{
    [SerializeField] private GameObject swarmPrefab;
    private bool swarmSpawned = false;

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