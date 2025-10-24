using UnityEngine;

public class JaguarAI : AnimalAIBase
{
    // Só herda o comportamento base

    protected override void OnChasingTarget()
    {
        Debug.Log("Jaguar is chasing you.");
    }
}
