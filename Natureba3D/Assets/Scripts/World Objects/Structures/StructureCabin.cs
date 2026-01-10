using UnityEngine;

public class StructureCabin : Structure
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot != null)
            return;

        base.Use(player, handSlot);

        // Se o objetivo do dia tiver sido concluído
        if (GameManager.Instance.objectiveWasCompleted)
        {
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você vai dormir."));
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você precisa completar todos os objetivos primeiro."));
        }
    }
}