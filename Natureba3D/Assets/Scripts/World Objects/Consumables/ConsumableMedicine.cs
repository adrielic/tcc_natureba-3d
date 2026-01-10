using UnityEngine;

public class ConsumableMedicine : Consumable
{
    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);
        
        GameManager.Instance.UpdateObjective('m');
        GameUIManager.Instance.UpdateObjetiveDisplay('m');
    }
}
