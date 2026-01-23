using UnityEngine;

public class Medicine : Consumable
{
    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);
        
        GameManager.Instance.CheckObjective("medicine");
    }
}
