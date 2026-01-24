using UnityEngine;

// Classe dos animais não hostis ao jogador
public class NonHostile : Entity
{
    protected override void DetectTarget()
    {
        // Nenhum comportamento específico
        base.DetectTarget();
    }

    protected override void OnContact(Collider hit)
    {
        // Nenhum comportamento específico
        base.OnContact(hit);
    }
}
