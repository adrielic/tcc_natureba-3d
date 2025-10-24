using UnityEngine;

public class WolfAI : AnimalAIBase
{
    // Só herda o comportamento base

    protected override void OnChasingTarget()
    {
        Debug.Log("Wolf is chasing you.");
    }
}
