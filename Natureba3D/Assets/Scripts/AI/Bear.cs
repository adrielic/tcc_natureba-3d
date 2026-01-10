using UnityEngine;

public class Bear : Entity
{
    // Só herda o comportamento base

    protected override void OnChasingTarget()
    {
        base.OnChasingTarget();
        Invoke("ChangeSpeeds", 2f);
    }

    void ChangeSpeeds()
    {
        agent.speed = 20;
        agent.angularSpeed = 200;
        agent.acceleration = 50;
    }
}
