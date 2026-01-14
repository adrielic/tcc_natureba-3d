using UnityEngine;

public class Wolf : Entity
{
    protected override void OnChasingTarget()
    {
        base.OnChasingTarget();
        
        agent.speed = 8;
        agent.angularSpeed = 200;
        agent.acceleration = 50;
    }
}
