using System.Collections;
using UnityEngine;

public class Campfire : Structure
{
    [SerializeField] private bool isLit = false;
    [SerializeField] private GameObject logs;
    [SerializeField] private float timeToBurnOff;

    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        if (itemInHands == null) return;

        base.Use(player, itemInHands);

        if (itemInHands is WoodenLog && !isLit)
        {
            isLit = true;
            gameObject.name = "Campfire (Lit)";
            logs.SetActive(true);
            StartCoroutine(BurnOff());

            Destroy(itemInHands.gameObject);
            player.ClearHands();
        }
        else if (itemInHands is Food fish && !fish.isEdible && isLit)
        {
            fish.Cook();
        }
    }

    private IEnumerator BurnOff()
    {
        yield return new WaitForSeconds(timeToBurnOff);
        isLit = false;
        gameObject.name = "Campfire";
        logs.SetActive(false);
    }
}
