using UnityEngine;

public class ConsumableWater : Consumable
{
    public enum CanteenFullness { Full, Empty };
    public CanteenFullness fullness;

    public override void Consume(PlayerInteraction player)
    {
        if (fullness == CanteenFullness.Full)
        {
            base.Consume(player);

            GameManager.Instance.CheckObjective('w');
            GameUIManager.Instance.UpdateObjetiveDisplay('w');
        }
        else
            StartCoroutine(GameUIManager.Instance.ShowFeedback("O cantil está vazio."));
    }
}
