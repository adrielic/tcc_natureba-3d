using UnityEngine;
public class DeadTree : Interactable
{
    public override void Use(PlayerInteraction player, Interactable target)
    {
        if (target != null) return;

        StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você derrubou a árvore."));
        Debug.Log("The dead tree has fallen.");
    }
}