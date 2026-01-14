using UnityEngine;

public class ConsumableFood : Consumable
{
    public bool isEdible;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);

        if (isEdible)
        {
            GameManager.Instance.CheckObjective('f');
            GameUIManager.Instance.UpdateObjetiveDisplay('f');
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ShowFeedback("Você morreu."));
            GameManager.Instance.GameOver("Intoxication_Fish");
        }
    }
}
