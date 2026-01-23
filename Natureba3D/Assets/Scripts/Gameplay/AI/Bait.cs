using UnityEngine;

// Classe dos objetos que servem como isca para os animais
public class Bait : MonoBehaviour
{
    public enum BaitType { Carnivore, Herbivore }
    public BaitType type; // Diferencia o tipo de isca para diferentes animais
    public bool isEnabled; // Ativado apenas após o jogador soltar a isca no chão

    // Quando uma isca é pega pelo animal
    public void OnBaitTaken()
    {
        // Garantindo que a interação do jogador com a isca seja desativada se o animal já pegou a isca
        if (TryGetComponent(out Interactable interactable))
        {
            interactable.DisableInteraction();
        }
    }
}
