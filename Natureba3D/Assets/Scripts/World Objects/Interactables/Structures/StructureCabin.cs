using UnityEngine;

public class StructureCabin : Structure
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot != null)
            return;

        base.Use(player, handSlot);

        // Se o objetivo do dia tiver sido concluído
        if (GameManager.Instance.objectiveComplete)
        {
            GameManager.Instance.FinishLevel();
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ShowFeedback("Você precisa completar todos os objetivos primeiro."));
        }
    }
}