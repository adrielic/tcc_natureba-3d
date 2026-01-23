using UnityEngine;

public class Toxin : Consumable
{
    [SerializeField] private enum ToxinEffects { Death, Hallucination };
    [SerializeField] ToxinEffects effect;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);
        
        switch (effect)
        {
            case ToxinEffects.Death:
                GameManager.Instance.GameOver("Intoxication_Mushroom");
                break;
            case ToxinEffects.Hallucination:
                Debug.Log("The player is hallucinating.");
                break;
        }
    }
}
