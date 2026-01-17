public class Cougar : Entity
{
    protected override void OnChasingTarget()
    {
        base.OnChasingTarget();
        
        agent.speed = 10;
        agent.angularSpeed = 360;
        agent.acceleration = 50;
    }
}
