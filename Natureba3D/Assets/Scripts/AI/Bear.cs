using UnityEngine;

public class Bear : Entity
{
    protected override void OnChasingTarget()
    {
        base.OnChasingTarget();
        
        Invoke("ChangeSpeeds", 2f);
    }

    void ChangeSpeeds()
    {
        agent.speed = 8;
        agent.angularSpeed = 360;
        agent.acceleration = 50;
    }
}
