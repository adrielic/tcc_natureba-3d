using UnityEngine;
public class Cabin : Interactable
{
    public override void Use(PlayerInteraction player, Interactable handSlot)
    {
        if (handSlot != null) return;

        // Se o objetivo do dia tiver sido concluído
        if (GameManager.Instance.objectiveWasCompleted)
        {
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você vai dormir."));
            Debug.Log("You go to sleep.");
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você precisa completar todos os objetivos primeiro."));
            Debug.Log("You need to do all your objectives first.");
        }
    }
}