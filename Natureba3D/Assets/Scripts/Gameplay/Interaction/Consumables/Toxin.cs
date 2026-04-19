using System;
using UnityEngine;

public class Toxin : Consumable
{
    private enum ToxinEffects { Death, Hallucination };
    [SerializeField] private ToxinEffects effect;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);

        switch (effect)
        {
            case ToxinEffects.Death:
                GameManager.Instance.GameOver("Intoxication_Mushroom");
                break;
            case ToxinEffects.Hallucination:
                player.Hallucinate();
                break;
        }
    }
}
