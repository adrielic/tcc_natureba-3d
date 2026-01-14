using UnityEngine;

public class Cougar : Entity
{
    protected override void OnChasingTarget()
    {
        base.OnChasingTarget();
        
        agent.speed = 15;
        agent.angularSpeed = 200;
        agent.acceleration = 50;
    }
}
