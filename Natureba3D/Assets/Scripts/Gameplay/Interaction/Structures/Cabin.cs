using UnityEngine;

public class Cabin : Structure
{
    public override void Use(PlayerInteraction player, Interactable itemInHands)
    {
        if (itemInHands != null) return;

        base.Use(player, itemInHands);

        if (GameManager.Instance.objectiveIsComplete)
        {
            GameManager.Instance.FinishLevel();
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ShowNotification("Você precisa completar todos os objetivos primeiro."));
        }
    }
}