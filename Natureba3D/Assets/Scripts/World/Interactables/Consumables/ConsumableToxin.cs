using UnityEngine;

public class ConsumableToxin : Consumable
{
    public enum ToxinEffects { Death, Hallucination };
    public ToxinEffects effect;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);
        
        switch (effect)
        {
            case ToxinEffects.Death:
                StartCoroutine(GameUIManager.Instance.ShowFeedback("Você morreu."));
                GameManager.Instance.GameOver("Intoxication_Mushroom");
                break;
            case ToxinEffects.Hallucination:
                StartCoroutine(GameUIManager.Instance.ShowFeedback("Você está alucinando."));
                break;
        }
    }
}
