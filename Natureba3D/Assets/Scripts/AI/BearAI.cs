using UnityEngine;

public class BearAI : AnimalAIBase
{
    // Só herda o comportamento base

    protected override void OnChasingTarget()
    {
        Debug.Log("Bear is chasing you.");
    }
}
