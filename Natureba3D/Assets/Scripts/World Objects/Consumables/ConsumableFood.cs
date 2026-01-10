using UnityEngine;

public class ConsumableFood : Consumable
{
    public bool isEdible;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);

        if (isEdible)
        {
            GameManager.Instance.UpdateObjective('f');
            GameUIManager.Instance.UpdateObjetiveDisplay('f');
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você morreu."));
        }
    }
}
