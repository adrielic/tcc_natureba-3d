using UnityEngine;

public class Consumable : Interactable
{
    public enum ConsumableType { Apple, FishRaw, FishCooked, MushroomBrown, MushroomRed, MushroomYellow, MushroomPurple, CanteenFull, CanteenEmpty }
    public ConsumableType type;

    public void Consume(PlayerInteraction player)
    {
        switch (type)
        {
            case ConsumableType.Apple:
                GameManager.Instance.UpdateObjective('f');
                GameUIManager.Instance.UpdateObjetiveDisplay('f');
                Debug.Log("You ate the apple.");
                break;
            case ConsumableType.FishRaw:
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você morreu."));
                Debug.Log("You ate the raw fish and died of intoxication.");
                break;
            case ConsumableType.FishCooked:
                GameManager.Instance.UpdateObjective('f');
                GameUIManager.Instance.UpdateObjetiveDisplay('f');
                Debug.Log("You ate the cooked fish.");
                break;
            case ConsumableType.MushroomBrown:
                GameManager.Instance.UpdateObjective('f');
                GameUIManager.Instance.UpdateObjetiveDisplay('f');
                Debug.Log("You ate the brown mushroom.");
                break;
            case ConsumableType.MushroomRed:
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você está alucinando."));
                Debug.Log("You ate the red mushroom and are allucinating.");
                break;
            case ConsumableType.MushroomYellow:
                GameManager.Instance.UpdateObjective('h');
                GameUIManager.Instance.UpdateObjetiveDisplay('h');
                Debug.Log("You ate the yellow mushroom.");
                break;
            case ConsumableType.MushroomPurple:
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("Você morreu."));
                Debug.Log("You ate the purple mushroom and died of intoxication.");
                break;
            case ConsumableType.CanteenFull:
                GameManager.Instance.UpdateObjective('w');
                GameUIManager.Instance.UpdateObjetiveDisplay('w');
                Debug.Log("You drank the water.");
                break;
            case ConsumableType.CanteenEmpty:
                StartCoroutine(GameUIManager.Instance.ChangeFeedbackText("O cantil está vazio."));
                Debug.Log("The canteen is empty.");
                return;
        }

        Destroy(gameObject);
        player.ClearHands();
    }
}
