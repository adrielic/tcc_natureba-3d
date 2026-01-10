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

            GameManager.Instance.UpdateObjective('w');
            GameUIManager.Instance.UpdateObjetiveDisplay('w');
        }
        else
            StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("O cantil está vazio."));
    }
}
