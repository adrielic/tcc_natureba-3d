using UnityEngine;

public class StructureDeadTree : Structure
{
    public override void Use(PlayerInteraction player, Interactable target)
    {
        if (target != null)
            return;

        base.Use(player, target);

        StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você derrubou a árvore."));
    }
}