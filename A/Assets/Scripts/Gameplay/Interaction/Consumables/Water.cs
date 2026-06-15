using UnityEngine;

public class Water : Consumable
{
    public enum CanteenFullness { Full, Empty };
    public CanteenFullness fullness;
    [SerializeField] private Material[] fullState;
    [SerializeField] private Material[] emptyState;

    void Start()
    {
        if (fullness == CanteenFullness.Empty)
        {
            meshRenderer.materials = emptyState;
        }
    }

    public override void Consume(PlayerInteraction player)
    {
        if (fullness == CanteenFullness.Full)
        {
            base.Consume(player);

            SwitchState(CanteenFullness.Empty);
            GameManager.Instance.CheckObjective("water");
        }
        else
        {
            StartCoroutine(GameUIManager.Instance.ShowNotification("O cantil está vazio."));
        }
    }

    public void SwitchState(CanteenFullness currentState)
    {
        fullness = currentState;

        switch (currentState)
        {
            case CanteenFullness.Full:
                gameObject.name = "Canteen (Full)";
                meshRenderer.materials = fullState;
                break;
            case CanteenFullness.Empty:
                gameObject.name = "Canteen (Empty)";
                meshRenderer.materials = emptyState;
                break;
        }
    }
}
