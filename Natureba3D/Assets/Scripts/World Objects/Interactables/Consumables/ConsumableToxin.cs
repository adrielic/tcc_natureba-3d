using UnityEngine;

public class ConsumableToxin : Consumable
{
    public enum ToxinType { Poison, Hallucination };
    public ToxinType type;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);
        
        switch (type)
        {
            case ToxinType.Poison:
                StartCoroutine(GameUIManager.Instance.ShowFeedback("Você morreu."));
                GameManager.Instance.GameOver("Intoxication_Mushroom");
                break;
            case ToxinType.Hallucination:
                StartCoroutine(GameUIManager.Instance.ShowFeedback("Você está alucinando."));
                break;
        }
    }
}
