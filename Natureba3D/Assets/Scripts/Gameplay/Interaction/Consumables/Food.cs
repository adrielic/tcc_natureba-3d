using UnityEditor;
using UnityEngine;

public class Food : Consumable
{
    [SerializeField] private Material[] cooked;
    public bool isEdible;

    public override void Consume(PlayerInteraction player)
    {
        base.Consume(player);

        if (isEdible)
        {
            GameManager.Instance.CheckObjective("food");
        }
        else
        {
            GameManager.Instance.GameOver("Intoxication_Fish");
        }
    }

    public void Cook()
    {
        isEdible = true;
        gameObject.name = "Fish (Cooked)";
        meshRenderer.materials = cooked;
    }
}
